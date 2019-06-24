using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpawn : MonoBehaviour
{

    private Transform startTransfrom;
    private float distance;

    void Start()
    {
        startTransfrom = transform;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        distance += Time.deltaTime / 50;

        transform.position = startTransfrom.position + transform.forward * distance;
        transform.rotation = startTransfrom.rotation;
    }
}
