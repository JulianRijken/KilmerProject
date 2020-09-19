using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Train : MonoBehaviour
{

    [SerializeField] private TrainSettings settings;

    private GameInput controls;
    private Rigidbody rig;
    private GameManager gameManager;
    private List<Wagon> wagons = new List<Wagon>();
    private List<BufferTransform> bufferTransforms = new List<BufferTransform>();

    private bool inHomeStation = false;
    private bool spawning = true;
    private bool gettingRemoved = false;
    private int totalWagonDistance = -5;
    private float lifeTime = 0;
    private float steerAxis;


    private void Awake()
    {
        controls = new GameInput();

        controls.Player.SteerAxis.performed += context => steerAxis = context.ReadValue<float>();
        controls.Player.SteerAxis.canceled += context => steerAxis = 0;
    }

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        rig = GetComponent<Rigidbody>();
        gameObject.layer = 15;
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;

        if (spawning)        
            transform.position += transform.forward * Time.deltaTime * settings.global.moveSpeed;        
        else if (inHomeStation == false)        
            Rotate();
        
        MoveWagons();
    }

    private void FixedUpdate()
    {
        Move();
        HandleBufferList();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (spawning == false && inHomeStation == false)
        {
            HomeStation station = collision.GetComponent<HomeStation>();

            if (station != null && inHomeStation == false)
            {
                if (wagons.Count == 0)               
                    OnHitObstacal();               
                else            
                    EnterStation(station);          
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (spawning == false && inHomeStation == false)
        {
            if (collision.gameObject.layer == 12 || collision.gameObject.layer == 11)           
                OnHitObstacal();           
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (spawning == true)
        {
            spawning = false;
            gameObject.layer = 11;
        }
    }


    /// <summary>
    /// Handles The buffer list for the wagons
    /// </summary>
    private void HandleBufferList()
    {
        if (gettingRemoved == false)
        {
            while (bufferTransforms.Count <= settings.global.bufferSize)
            {
                bufferTransforms.Add(new BufferTransform(transform.position, transform.GetChild(0).rotation));
            }
        }

        while (bufferTransforms.Count > settings.global.bufferSize)
        {
            bufferTransforms.RemoveAt(0);
        }
    }

    /// <summary>
    /// Adds a wagon to the train
    /// </summary>
    public void AddWagon()
    {
        Wagon wagon = Instantiate(settings.wagonPrefab).GetComponent<Wagon>();
        totalWagonDistance = wagon.distance + totalWagonDistance;
        wagon.distance = totalWagonDistance;
        wagons.Add(wagon);
        MoveWagons();
        GameUI.instance.ShowStationArrowInfo();

        if (settings.global.WagonAddEffect != null)
            Instantiate(settings.global.WagonAddEffect, wagon.transform.position + (wagon.transform.up * 0.5f), Quaternion.Euler(Vector3.up), wagon.transform);

    }

    /// <summary>
    /// Moves the train wagons
    /// </summary>
    private void MoveWagons()
    {
        int globalBufferSize = settings.global.bufferSize;

        for (int i = 0; i < wagons.Count; i++)
        {
            if (wagons[i] != null)
            {
                wagons[i].transform.position = bufferTransforms[globalBufferSize - wagons[i].distance].position;
                wagons[i].transform.rotation = bufferTransforms[globalBufferSize - wagons[i].distance].rotation;
            }
        }
    }

    /// <summary>
    /// Rotates The train
    /// </summary>
    private void Rotate()
    {
        if (gettingRemoved == false)
        {
            if (gameManager.GetGameState().Equals(GameState.Playing))
            {
                // Curve
                float zRot = steerAxis * settings.global.curveMultiple;
                Quaternion toRot = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRot));
                transform.GetChild(0).transform.rotation = Quaternion.Slerp(transform.GetChild(0).transform.rotation, toRot, Time.deltaTime / settings.global.curveRecoverTime);

                // Rotate y
                transform.Rotate(0, steerAxis * settings.global.rotationSpeed * Time.deltaTime * 100, 0);
            }
        }
    }

    /// <summary>
    /// Moves The train
    /// </summary>
    private void Move()
    {
        // Always move player forward
        if (spawning == false)
        {
            rig.velocity = transform.forward * settings.global.moveSpeed;
        }
    }

    /// <summary>
    /// Destroys the player and the carts
    /// </summary>
    private void OnHitObstacal()
    {
        if (gettingRemoved == false)
            StartCoroutine(KillTrain());
    }

    /// <summary>
    /// Makes a new train spawn and makes the current train slowly die
    /// </summary>
    private void EnterStation(HomeStation station)
    {
        if (spawning == false)
        {
            gameManager.RemoveCinemachineTargetGroupTarget(transform);
            inHomeStation = true;
            transform.rotation = Quaternion.Euler(0, station.InRotation.transform.eulerAngles.y, 0);
            gameManager.SpawBus(settings.playerId, station, wagons.Count);

            float timeToDestroy = ((wagons.Count * 0.25f) * (settings.global.moveSpeed / 10)) + 1;
            for (int i = 0; i < wagons.Count; i++)
            {
                Destroy(wagons[i].gameObject, timeToDestroy);
            }
            Destroy(gameObject, timeToDestroy);

        }
    }


    /// <summary>
    /// Removes The Train
    /// </summary>
    private IEnumerator KillTrain()
    {
        gettingRemoved = true;

        DeadVehicle go = Instantiate(settings.global.TrainDeathEffect, transform.position, transform.rotation).GetComponent<DeadVehicle>();
        if (go != null)
            go.SetVelocity(rig.velocity);

        GetComponent<BoxCollider>().enabled = false;
        Destroy(transform.GetChild(0).gameObject);


        for (int i = 0; i < wagons.Count; i++)
        {
            yield return new WaitForSeconds(0.03f);

            Instantiate(settings.global.WagonDeathEffect, wagons[i].transform.position, wagons[i].transform.rotation);

            if (Random.Range(0, 2) == 0)
            {
                Rigidbody passangerRig = Instantiate(settings.global.passenger, wagons[i].transform.position, wagons[i].transform.rotation).GetComponent<Rigidbody>();

                passangerRig.AddForce(Vector3.up * 5, ForceMode.Impulse);
            }

            Destroy(wagons[i].gameObject);
        }


        gameManager.RemoveCinemachineTargetGroupTarget(transform);

        gameManager.SpawnTrain(settings.playerId, 5);

        Destroy(gameObject);
    }


    /// <summary>
    /// Returns the LifeTime
    /// </summary>
    public float GetLifeTime()
    {
        return lifeTime;
    }

    /// <summary>
    /// Returns true if the train is currently spawning
    /// </summary>
    public bool GetSpawning()
    {
        return spawning;
    }

    /// <summary>
    /// Returns a list of all the wagons
    /// </summary>
    public List<Wagon> GetWagons()
    {
        return wagons;
    }

}



public class BufferTransform
{
    public BufferTransform(Vector3 _position, Quaternion _rotation)
    {
        position = _position;
        rotation = _rotation;
    }

    public Vector3 position;
    public Quaternion rotation;
}
