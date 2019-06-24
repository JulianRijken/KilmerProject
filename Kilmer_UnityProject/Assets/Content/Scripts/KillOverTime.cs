using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOverTime : MonoBehaviour
{

    public float time;

    void Start()
    {
        Destroy(gameObject, time);
    }

}
