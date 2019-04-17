using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private float offset = -0.5f;
    [SerializeField] private float scale = 0.4f;
    [SerializeField] private float speed = 1f;


    void Update()
    {
        transform.position = new Vector3(transform.position.x, (Mathf.Sin(Time.time * speed) * scale) + offset, transform.position.z);
    }
}
