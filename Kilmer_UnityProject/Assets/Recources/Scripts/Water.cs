using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private float scale;
    [SerializeField] private float speed;


    void Update()
    {
        transform.position = new Vector3(transform.position.x, (Mathf.Sin(Time.time * speed) * scale) + offset, transform.position.z);
    }
}
