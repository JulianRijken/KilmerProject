using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{   
    public BusSettings settings;
    private GameManager gameManager;
    private Rigidbody rig;
    private WheelCollider[] wheels;

    private int points;

    private bool spawning = true;

    private float lifeTime;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        wheels = GetComponentsInChildren<WheelCollider>();
        rig.constraints = RigidbodyConstraints.FreezeAll;
        spawning = true;

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        lifeTime = 0;

    }

    void Update()
    {
        lifeTime += Time.deltaTime;

        if (spawning)
            Spawn();
        else
            Move();
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

    public void SetPoints(int c_points)
    {
        points = c_points;
    }


    // Rotates te wheels
    void Move()
    {

        // Move weels
        for (int i = 0; i < wheels.Length; i++)
        {
            if (wheels[i].transform.localPosition.z > 0)
            {
                wheels[i].steerAngle = GetKeyInput().x * settings.rotateAngle;
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
                    wheels[i].motorTorque = wheels[i].steerAngle = GetKeyInput().y * settings.moveSpeed;

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


    // Get the key input
    Vector2 GetKeyInput()
    {
        Vector2 inputVector = new Vector2(0,0);

        if (Input.GetKey(settings.leftKey))
            inputVector.x -= 1;
        if (Input.GetKey(settings.rightKey))
            inputVector.x += 1;

        if (Input.GetKey(settings.upKey))
            inputVector.y += 1;
        if (Input.GetKey(settings.downKey))
            inputVector.y -= 1;

        return inputVector;
    }


    // Make bus move out of the HomeStation
    void Spawn()
    {
        transform.position += transform.forward * Time.deltaTime * 10;
    }

    void EindSpawn()
    {
        spawning = false;
        rig.constraints = RigidbodyConstraints.None;
        rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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


    private void KillBus()
    {
        gameManager.SpawnTrain(settings.playerId, 5);
        Destroy(gameObject);
    }

    public void EnterStation()
    {
        gameManager.SpawnTrain(settings.playerId, 1);
        Destroy(gameObject);
    }

}
