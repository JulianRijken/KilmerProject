using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Global Train Settings", menuName = "Global Train Settings")]
public class GlobalTrainSettings : ScriptableObject
{
    public float rotationSpeed = 2;
    public float moveSpeed = 10;
    public float curveRecoverTime = 0.1f;
    public float curveMultiple = 10;
    public int bufferSize = 10000;

    public GameObject TrainDeathEffect = null;
    public GameObject WagonDeathEffect = null;
    public GameObject WagonAddEffect = null;
    public GameObject passenger = null;
}
