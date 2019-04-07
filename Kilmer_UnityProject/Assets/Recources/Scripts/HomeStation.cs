using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeStation : MonoBehaviour
{

    private MeshRenderer meshRender;
    private Color startColor;
 
    public Transform busSpawnPoint;
    public Transform trainSpawnPoint;

    public Transform InRotation;
    public Transform OutRotation;

    public Transform noEntrancePlane;

    [HideInInspector] public bool use;


    private void Start()
    {
        meshRender = noEntrancePlane.GetComponent<MeshRenderer>();
        startColor = meshRender.material.color;
    }

    public void UseStation()
    {
        if (use == false)
            StartCoroutine(SetStationUse());
    }

    private IEnumerator SetStationUse()
    {
        use = true;
        yield return new WaitForSeconds(2);
        use = false;
    }





    void Update()
    {
        float alpha = (IsColliding() ? 1 : 0);
        Color color = startColor;
        color.a = Mathf.Lerp(meshRender.material.color.a, alpha,Time.deltaTime / 0.1f);
        meshRender.material.color = color;

    }


    /// <summary>
    /// Checks collison
    /// </summary>
    /// <returns></returns>
    bool IsColliding()
    {
        Collider[] hits = Physics.OverlapSphere(noEntrancePlane.position, 6);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].tag == "CamTarget")
            {
                Train train = hits[i].GetComponent<Train>();
                Bus bus = hits[i].GetComponent<Bus>();

                if (train != null)
                    if(train.GetSpawning() == false)
                        if(train.GetLifeTime() >= 2.5f)
                            if(train.wagons.Count == 0)
                                return true;

                if (bus != null)
                    if (bus.GetSpawning() == false)
                        if (bus.GetLifeTime() >= 2.5f)              
                                return true;


            }
        }

        return false;
    }


#if (UNITY_EDITOR)
    private void OnDrawGizmos()
    {
        // Draw Train Spawn
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(busSpawnPoint.position, 0.5f);

        // Draw Train Arrow
        Gizmos.DrawLine(InRotation.position, InRotation.position - InRotation.transform.forward * 2);
        Gizmos.DrawLine(InRotation.position, InRotation.position - InRotation.transform.forward / 2 - InRotation.transform.right / 2);
        Gizmos.DrawLine(InRotation.position, InRotation.position - InRotation.transform.forward / 2 + InRotation.transform.right / 2);


        // Draw Bus Spawn
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawSphere(trainSpawnPoint.position, 0.5f);

        // Draw Bus Arrow
        Gizmos.DrawLine(OutRotation.position, OutRotation.position - OutRotation.transform.forward * 2);
        Gizmos.DrawLine(OutRotation.position, OutRotation.position - OutRotation.transform.forward / 2 - OutRotation.transform.right / 2);
        Gizmos.DrawLine(OutRotation.position, OutRotation.position - OutRotation.transform.forward / 2 + OutRotation.transform.right / 2);
    }

#endif
}
