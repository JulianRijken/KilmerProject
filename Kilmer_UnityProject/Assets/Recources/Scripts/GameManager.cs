using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;


public enum PlayerId
{
    playerOne = 0,
    playerTwo = 1,
    playerThree = 2,
    playerFour = 3,
}

public enum GameState
{
    Menu = 0,
    Playing = 1,
    pause = 2,
    winScreen = 3   
}

public class GameManager : MonoBehaviour
{

    [SerializeField] private VehiclePrefabs vehiclePrefabs = null;
    [SerializeField] private MainMenu mainMenu = null;

    private List<HomeStation> homeStations = new List<HomeStation>();
    private CinemachineTargetGroup cinemachineTargetGroup;

    private GameState gameState;

    private List<GameObject> inGameVehicles = new List<GameObject>();

    void Start()
    {
        cinemachineTargetGroup = FindObjectOfType<CinemachineTargetGroup>();
        SetGameState(GameState.Menu);

        // Get all stations
        GameObject[] go = GameObject.FindGameObjectsWithTag("HomeStation");
        for (int i = 0; i < go.Length; i++)
        {
            homeStations.Add(go[i].GetComponent<HomeStation>());
        }
    }


    /// <summary>
    /// Starts the game
    /// </summary>
    public void StartGame(int players)
    {
        StartCoroutine(IStartGame(players));
    }
    private IEnumerator IStartGame(int players)
    {
        SetGameState(GameState.Playing);

        yield return new WaitForSeconds(1);

        mainMenu.gameObject.SetActive(false);

        for (int i = 0; i < players; i++)
        {
            SpawnTrain((PlayerId)i, 0);
        }
    }

    /// <summary>
    /// Spawns a bus at a random station
    /// </summary>
    public void SpawBus(PlayerId playerId,int station,int points)
    {
        Bus bus = Instantiate(GetBusPrefab(playerId), homeStations[station].busSpawnPoint.position, homeStations[station].OutRotation.rotation).GetComponent<Bus>();

        inGameVehicles.Add(bus.gameObject);

        bus.SetPoints(points);

        // Add to follow goup
        cinemachineTargetGroup.AddMember(bus.transform, 1, 0);

    }
    public void SpawBus(PlayerId playerId, HomeStation station, int points)
    {
        Bus bus = Instantiate(GetBusPrefab(playerId), station.busSpawnPoint.position, station.OutRotation.rotation).GetComponent<Bus>();

        inGameVehicles.Add(bus.gameObject);

        bus.SetPoints(points);

        // Add to follow goup
        cinemachineTargetGroup.AddMember(bus.transform, 1, 0);

    }

    /// <summary>
    /// Spawns a train at a free station
    /// </summary>
    public void SpawnTrain(PlayerId playerId,float waitTime = 5f)
    {
        StartCoroutine(ISpawnTrain(playerId, waitTime));
    }
    private IEnumerator ISpawnTrain(PlayerId playerId, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // Maak een lijst met de stations die avalible zijn

        while (GetFreeStations().Count == 0)
        {
            print("Waiting for spawn");
            yield return null;
        }

        List<HomeStation> freeStations = GetFreeStations();

        HomeStation spawnStation = freeStations[Random.Range(0, freeStations.Count)];

        spawnStation.UseStation();

        GameObject instante = Instantiate(GetTrainPrefab(playerId), spawnStation.trainSpawnPoint.position, spawnStation.OutRotation.rotation);

        inGameVehicles.Add(instante);

        // Add to follow goup
        cinemachineTargetGroup.AddMember(instante.transform, 1, 0);
    }

    /// <summary>
    /// Returns A list of free home stations
    /// </summary>
    private List<HomeStation> GetFreeStations()
    {
        List<HomeStation> stations = new List<HomeStation>();
        stations.Clear();

        for (int i = 0; i < homeStations.Count; i++)
        {
            if (homeStations[i].use == false)
                stations.Add(homeStations[i]);
        }

        return stations;
    }

    /// <summary>
    /// Returns the cinema target group
    /// </summary>
    public CinemachineTargetGroup GetCinemaGroup()
    {
        return cinemachineTargetGroup;
    }

    /// <summary>
    /// Remeoves Target from cinema target group
    /// </summary>
    public void RemoveCinemachineTargetGroupTarget(Transform t)
    {
        List<CinemachineTargetGroup.Target> tempTargets = new List<CinemachineTargetGroup.Target>();
        CinemachineTargetGroup group = GetCinemaGroup();
        CinemachineTargetGroup.Target[] ct = group.m_Targets;
        for (int i = 0; i < ct.Length; i++)
        {
            if (ct[i].target != t && ct[i].target != null)
            {
                tempTargets.Add(ct[i]);
            }
        }

        group.m_Targets = tempTargets.ToArray();
    }

    /// <summary>
    /// Returns Train prefab
    /// </summary>
    private GameObject GetTrainPrefab(PlayerId playerId)
    {
        for (int i = 0; i < vehiclePrefabs.trainPrefabs.Count; i++)
            if (vehiclePrefabs.trainPrefabs[i].playerId == playerId)
                return vehiclePrefabs.trainPrefabs[i].prefab;


        Debug.LogError("playerId Prefab Does Not Exist");
        return null;
    }

    /// <summary>
    /// Returns Bus prefab
    /// </summary>
    private GameObject GetBusPrefab(PlayerId playerId)
    {
        for (int i = 0; i < vehiclePrefabs.busPrefabs.Count; i++)
            if (vehiclePrefabs.busPrefabs[i].playerId == playerId)
                return vehiclePrefabs.busPrefabs[i].prefab;


        Debug.LogError("playerId Prefab Does Not Exist");
        return null;
    }

    /// <summary>
    /// Returns GameState
    /// </summary>
    public GameState GetGameState()
    {
        return gameState;
    }

    /// <summary>
    /// Sets the game state
    /// </summary>
    public void SetGameState(GameState state)
    {
        gameState = state;
    }


}
