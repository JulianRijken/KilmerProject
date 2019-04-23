using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour
{
    private Transform cam;
    private Rigidbody rig;

    private void Awake()
    {
        cam = Camera.main.transform;
        transform.LookAt(cam);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        rig = GetComponent<Rigidbody>();
    }

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            rig.AddForce(Vector3.up * 3, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        transform.LookAt(cam);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Train train = collision.transform.GetComponent<Train>();

        if (train != null)
        {
            train.AddWagon();
            Destroy(gameObject);
        }
    }




}
