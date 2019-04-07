using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VehiclePrefabs",menuName = "VehiclePrefabs")]
public class VehiclePrefabs : ScriptableObject
{
    public List<TrainPrefab> trainPrefabs = new List<TrainPrefab>();
    public List<BusPrefab> busPrefabs = new List<BusPrefab>();
}

[System.Serializable]
public class TrainPrefab
{
    public PlayerId playerId;
    public GameObject prefab;
}

[System.Serializable]
public class BusPrefab
{
    public PlayerId playerId;
    public GameObject prefab;
}
