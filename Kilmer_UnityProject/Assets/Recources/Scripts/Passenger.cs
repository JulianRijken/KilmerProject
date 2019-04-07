using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour
{



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
