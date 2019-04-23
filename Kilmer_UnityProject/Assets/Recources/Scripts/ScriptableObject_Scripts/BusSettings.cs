using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bus Settings",menuName = "Bus Settings")]
public class BusSettings : ScriptableObject
{
    public float rotateAngle = 40;
    public float moveSpeed = 1000;
    public float maxVelocity = 1000;

    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;

    public PlayerId playerId;

    public GameObject deadBusPrefab;
    public GameObject finishPrefab;

}
