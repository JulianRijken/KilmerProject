using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bus : MonoBehaviour
{   
    [SerializeField] private BusSettings settings;
    [SerializeField] private CanvasGroup infoGroup = null;
    [SerializeField] private AudioSource engineSound = null;
    [SerializeField] private AudioSource hornSound = null;

    private GameManager gameManager;
    private Rigidbody rig;
    private WheelCollider[] wheels;
    private Animator animator;

    private int points;

    private bool spawning = true;
    private bool keyPressed = false;

    private float lifeTime;

    private GameInput controls;
    private float steerAxis;
    private float gasAxis;


    private void Awake()
    {
        controls = new GameInput();

        controls.Player.SteerAxis.performed += context => steerAxis = context.ReadValue<float>();
        controls.Player.SteerAxis.canceled += context => steerAxis = 0;

        controls.Player.GasAxis.performed += context => gasAxis = context.ReadValue<float>();
        controls.Player.GasAxis.canceled += context => gasAxis = 0;

        controls.Player.Horn.performed += HandleHorn;

        infoGroup.alpha = 0;
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        wheels = GetComponentsInChildren<WheelCollider>();
        rig.constraints = RigidbodyConstraints.FreezeAll;
        spawning = true;
        animator = GetComponent<Animator>();

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        lifeTime = 0;

        hornSound.PlayDelayed(0.6f);
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;

        if (spawning)
            Spawn();
        else
        {
            Move();
            infoGroup.alpha += Time.deltaTime * 3;
        }

        if(keyPressed == false)
            if (gasAxis != 0f)
            {
                animator.SetBool("Key", true);
                keyPressed = true;
            }

        engineSound.pitch = (rig.velocity.magnitude / settings.maxVelocity) + 1;

    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11)
        {
            KillBus();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (spawning == true)
        {
            EindSpawn();
            rig.velocity = transform.forward * 10;
        }
    }



    public float GetLifeTime()
    {
        return lifeTime;
    }

    public bool GetSpawning()
    {
        return spawning;
    }

    public int GetPoints()
    {
        return points;
    }

    public BusSettings GetSettings()
    {
        return settings;
    }

    public void SetPoints(int c_points)
    {
        points = c_points;
    }



    private void Move()
    {

        // Move weels
        for (int i = 0; i < wheels.Length; i++)
        {
            if (wheels[i].transform.localPosition.z > 0)
            {
                wheels[i].steerAngle = steerAxis * settings.rotateAngle;
            }
            else if (wheels[i].transform.localPosition.z < 0)
            {

                if (rig.velocity.magnitude > settings.maxVelocity)
                {
                    wheels[i].brakeTorque = 100;
                    wheels[i].motorTorque = 0;
                }
                else
                {
                    wheels[i].brakeTorque = 0;
                    wheels[i].motorTorque = wheels[i].steerAngle = gasAxis * settings.moveSpeed;

                }

            }

            Quaternion q;
            Vector3 p;
            wheels[i].GetWorldPose(out p, out q);

            Transform shapeTransform = wheels[i].transform.GetChild(0);
            shapeTransform.position = p;
            shapeTransform.rotation = q;

        }

    }


    private void Spawn()
    {
        transform.position += transform.forward * Time.deltaTime * 10;
    }

    private void EindSpawn()
    {
        spawning = false;
        rig.constraints = RigidbodyConstraints.None;
        rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }


    private void HandleHorn(InputAction.CallbackContext context)
    {
        hornSound.Play();
    }


    private void KillBus()
    {
        gameManager.SpawnTrain(settings.playerId, 5);
        DeadVehicle go = Instantiate(settings.deadBusPrefab, transform.position, transform.rotation).GetComponent<DeadVehicle>();

        if (go != null)
            go.SetVelocity(rig.velocity);

        Destroy(gameObject);
    }

    public void EnterStation()
    {
        gameManager.SpawnTrain(settings.playerId, 1);
        Instantiate(settings.finishPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
