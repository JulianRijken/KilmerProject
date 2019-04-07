using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject passengerPrefab = null;
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private int maxPassangers = 20;
    [SerializeField] private float timeBitweenSpawns = 1;

    private void Start()
    {
        StartCoroutine(SpawnConstructor());
    }


    IEnumerator SpawnConstructor()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBitweenSpawns);

            if (gameManager.GetGameState().Equals(GameState.Playing))
            {

                List<int> spawnPoints = GetFreeSpawnPoints();

                if (spawnPoints.Count != 0 && GetPassengersCount() < maxPassangers)
                {
                    int randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                    SpawnPassenger(randomSpawnPoint);
                }
                else
                {
                    Debug.Log("No Spawns");
                }

            }
            
        }
    }


    /// <summary>
    /// Reurns A list of free spwan points
    /// </summary>
    private List<int> GetFreeSpawnPoints()
    {

        List<int> childs = new List<int>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount == 0)
                childs.Add(i);
        }

        return childs;
    }

    /// <summary>
    /// Spawns the passenger
    /// </summary>
    private void SpawnPassenger(int child)
    {
        Instantiate(passengerPrefab, transform.GetChild(child).position, transform.GetChild(child).rotation,transform.GetChild(child));
    }

    /// <summary>
    /// Returns the passengers on spwan points count
    /// </summary>
    private int GetPassengersCount()
    {
        int count = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount != 0)
                count++;
        }

        return count;
    }



#if (UNITY_EDITOR) 
    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = new Color(1, 0, 0);
            Gizmos.DrawSphere(transform.GetChild(i).position, 0.2f);

        }
    }
#endif

}
