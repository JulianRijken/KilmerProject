using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private bool OnlyYAxis = false;
    private Camera camara;

    private void Awake()
    {
        camara = Camera.main;
    }

    void Update()
    {
        Vector3 lookPos = camara.transform.position - transform.position;
        if(OnlyYAxis)
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;
    }
}
