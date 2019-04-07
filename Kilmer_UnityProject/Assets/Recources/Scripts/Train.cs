using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{

    public TrainSettings settings;

    private GameManager gameManager;
    private Rigidbody rig;

    private bool inHomeStation = false;
    private bool spawning = true;

    [HideInInspector] public List<Wagon> wagons = new List<Wagon>();
    private List<BufferTransform> bufferTransforms = new List<BufferTransform>();
    private int totalWagonDistance;

    private float lifeTime;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        spawning = true;

        //AddWagon(test);

        gameObject.layer = 15;
        lifeTime = 0;
        totalWagonDistance = -5;

    }


    void Update()
    {
        lifeTime += Time.deltaTime;

        if (spawning)
        {
            transform.position += transform.forward * Time.deltaTime * settings.global.moveSpeed;
        }
        else if(inHomeStation == false)
        {
            Rotate();
        }

        MoveWagons();

    }

    public float GetLifeTime()
    {
        return lifeTime;
    }

    public bool GetSpawning()
    {
        return spawning;
    }

    private void AddToBufferList()
    {
        while (bufferTransforms.Count <= settings.global.bufferSize)
        {
            bufferTransforms.Add(new BufferTransform(transform.position, transform.GetChild(0).rotation));
        }
        while (bufferTransforms.Count > settings.global.bufferSize)
        {
            bufferTransforms.RemoveAt(0);
        }
    }

    private void MoveWagons()
    {
        int globalBufferSize = settings.global.bufferSize;

        for (int i = 0; i < wagons.Count; i++)
        {
            wagons[i].transform.position = bufferTransforms[globalBufferSize - wagons[i].distance].position;
            wagons[i].transform.rotation = bufferTransforms[globalBufferSize - wagons[i].distance].rotation;
        }
    }

    public void AddWagon()
    {
        Wagon wagon = Instantiate(settings.wagonPrefab).GetComponent<Wagon>();
        totalWagonDistance = wagon.distance + totalWagonDistance;
        wagon.distance = totalWagonDistance;
        wagons.Add(wagon);
        Instantiate(settings.global.WagonAddEffect, wagon.transform.position = bufferTransforms[settings.global.bufferSize - totalWagonDistance].position, wagon.transform.rotation = bufferTransforms[settings.global.bufferSize - totalWagonDistance].rotation);
        MoveWagons();
    }


    private void FixedUpdate()
    {      
        Move();

        AddToBufferList();
    }


    void Rotate()
    {
        if (gameManager.GetGameState().Equals(GameState.Playing))
        {
            // Curve

            float zRot = GetKeyInput() * settings.global.curveMultiple;
            Quaternion toRot = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRot));


            transform.GetChild(0).transform.rotation = Quaternion.Slerp(transform.GetChild(0).transform.rotation,toRot,Time.deltaTime / settings.global.curveRecoverTime);


            // Rotate y
            transform.Rotate(0, GetKeyInput() * settings.global.rotationSpeed * Time.deltaTime * 100, 0);
        }
    }

    void Move()
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
    void OnHitObstacal()
    {
        gameManager.RemoveCinemachineTargetGroupTarget(transform);

        // Sapwn new train
        gameManager.SpawnTrain(settings.playerId, 5);

        for (int i = 0; i < wagons.Count; i++)
        {
            Instantiate(settings.global.WagonDeathEffect, wagons[i].transform.position, wagons[i].transform.rotation);
            Destroy(wagons[i].gameObject);
        }

        Instantiate(settings.global.TrainDeathEffect, transform.position, transform.rotation);
        Destroy(gameObject);

    }

    /// <summary>
    /// Makes a new train spawn and makes the current train slowly die
    /// </summary>
    void EnterStation(HomeStation station)
    {
        if (spawning == false)
        {
            gameManager.RemoveCinemachineTargetGroupTarget(transform);
            inHomeStation = true;
            transform.rotation = Quaternion.Euler(0, station.InRotation.transform.eulerAngles.y, 0);
            gameManager.SpawBus(settings.playerId, Random.Range(0, 3),wagons.Count);

            float timeToDestroy = ((wagons.Count * 0.25f) * (settings.global.moveSpeed / 10)) + 1;
            for (int i = 0; i < wagons.Count; i++)
            {
                Destroy(wagons[i].gameObject, timeToDestroy);
            }
            Destroy(gameObject, timeToDestroy);

        }
    }

    /// <summary>
    /// Lets the train collide and die
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {

        if (spawning == false && inHomeStation == false)
        {
            if (collision.gameObject.layer == 12 || collision.gameObject.layer == 11)
            { 
                OnHitObstacal();
            }

        }
    }

    /// <summary>
    /// Lets the train enter the station and hit the station wall
    /// </summary>
    private void OnTriggerEnter(Collider collision)
    {
        if (spawning == false && inHomeStation == false)
        {
            HomeStation station = collision.GetComponent<HomeStation>();

            if (station != null && inHomeStation == false)
                if (wagons.Count == 0)
                {
                    OnHitObstacal();
                }
                else
                {
                    EnterStation(station);
                }
        }
    }

    /// <summary>
    /// Exits Spawn State
    /// </summary>
    private void OnTriggerExit(Collider collision)
    {
        if (spawning == true)
        {
            spawning = false;
            gameObject.layer = 11;
        }
    }

    /// <summary>
    /// Returns the keyboard Input as int
    /// </summary>
    int GetKeyInput()
    {
        int keyInput = 0;

        if (Input.GetKey(settings.leftKey))
            keyInput -= 1;
        if (Input.GetKey(settings.rightKey))
            keyInput += 1;

        return keyInput;
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
