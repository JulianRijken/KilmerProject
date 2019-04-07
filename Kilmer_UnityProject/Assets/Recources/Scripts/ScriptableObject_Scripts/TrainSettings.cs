using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Train Settings",menuName = "Trian Settings")]
public class TrainSettings : ScriptableObject
{
    public GlobalTrainSettings global;
    public GameObject wagonPrefab;
    public PlayerId playerId;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;


}
