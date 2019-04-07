using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Powerups
{
    speed = 0    
}

public class Powerup : MonoBehaviour
{
    public Powerups powerup;

    public float rotateSpeed;
    public float moveHight;

    void Update()
    {
        transform.GetChild(0).Rotate(0, rotateSpeed * Time.deltaTime, 0);
        transform.GetChild(0).localPosition = new Vector3(0, Mathf.Sin(Time.time) * moveHight, 0);
    }
}
