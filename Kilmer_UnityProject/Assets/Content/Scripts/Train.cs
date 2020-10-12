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
        // Subscribe the controls
        controls = new GameInput();
        controls.Player.SteerAxis.performed += context => steerAxis = context.ReadValue<float>();
        controls.Player.SteerAxis.canceled += context => steerAxis = 0;
    }

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // I'm looking at this from the future and today i would use a singleton for a gamemanager
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        gameObject.layer = 15;
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;

        if (spawning)
        {
            // just move the train foarward while its spawning
            transform.position += transform.forward * Time.deltaTime * settings.global.moveSpeed;
            return;
        }

        // Update the train
        Rotate();     
        Move();
        MoveWagons();
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
        // Make sure the train is driving around
        if (spawning == false && !inHomeStation)
        {
            // Get the home station
            HomeStation station = collision.GetComponent<HomeStation>();

            // Check if the trigger entered is a station
            if (station != null)
            {
                if (wagons.Count == 0)
                {
                    // If the train has no passangers desroy it
                    OnHitObstacal();
                }
                else
                {
                    // Else enter the station
                    EnterStation(station);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Make sure the train is driving around
        if (!spawning && !inHomeStation)
        {
            // Check if the collision is done on the right layer
            if (collision.gameObject.layer == 12 || collision.gameObject.layer == 11)
                OnHitObstacal();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        // If the train exits the spawn trigger and is still spawning set spawning false
        if (spawning)
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
        // Check if the train is not getting removed
        if (!gettingRemoved)
        {
            // Make sure the buffer stays full
            while (bufferTransforms.Count <= settings.global.bufferSize)
            {
                bufferTransforms.Add(new BufferTransform(transform.position, transform.GetChild(0).rotation));
            }
        }

        // Make sure the buffer stays the same size
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
        // Get the component
        Wagon wagon = Instantiate(settings.wagonPrefab).GetComponent<Wagon>();

        // Calculate the distance
        totalWagonDistance = wagon.distance + totalWagonDistance;
        wagon.distance = totalWagonDistance;

        // Add the wagon to the list of wagons
        wagons.Add(wagon);

        // Update the position of all the wagons so it does stay a frame behind
        MoveWagons();

        // Show the arrow tip
        GameUI.instance.ShowStationArrowInfo();

        // Add a particle effect when spawning the wagon
        if (settings.global.WagonAddEffect != null)
            Instantiate(settings.global.WagonAddEffect, wagon.transform.position + (wagon.transform.up * 0.5f), Quaternion.Euler(Vector3.up), wagon.transform);

    }

    /// <summary>
    /// Moves the train wagons
    /// </summary>
    private void MoveWagons()
    {
        // Move the wagons to the correct position
        for (int i = 0; i < wagons.Count; i++)
        {
            if (wagons[i] != null)
            {
                wagons[i].transform.position = bufferTransforms[settings.global.bufferSize - wagons[i].distance].position;
                wagons[i].transform.rotation = bufferTransforms[settings.global.bufferSize - wagons[i].distance].rotation;
            }
        }
    }

    /// <summary>
    /// Rotates The train
    /// </summary>
    private void Rotate()
    {
        if (!gettingRemoved && !inHomeStation)
        {
            if (gameManager.GetGameState().Equals(GameState.Playing))
            {
                // Makes the train curve to the sides when driving in a corner
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
        // Updates the trains velocity and makes it move foarward
        rig.velocity = transform.forward * settings.global.moveSpeed;      
    }

    /// <summary>
    /// Destroys the player and the carts
    /// </summary>
    private void OnHitObstacal()
    {
        if (!gettingRemoved)
            StartCoroutine(KillTrain());
    }

    /// <summary>
    /// Makes a new train spawn and makes the current train slowly die
    /// </summary>
    private void EnterStation(HomeStation station)
    {
        if (!spawning)
        {
            inHomeStation = true;
            gameManager.RemoveCinemachineTargetGroupTarget(transform);

            // Makes sure the train drives in a straight line
            transform.rotation = Quaternion.Euler(0, station.InRotation.transform.eulerAngles.y, 0);

            // Spawn the bus
            gameManager.SpawBus(settings.playerId, station, wagons.Count);

            // Destroy the train
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

        // Show a destroyed version of the train
        DeadVehicle deadVehicle = Instantiate(settings.global.TrainDeathEffect, transform.position, transform.rotation).GetComponent<DeadVehicle>();

        if (deadVehicle != null)
            deadVehicle.SetVelocity(rig.velocity);

        GetComponent<BoxCollider>().enabled = false;
        Destroy(transform.GetChild(0).gameObject);

        // Destroy all the wagons one by one
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

        // Spawn a new train
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
