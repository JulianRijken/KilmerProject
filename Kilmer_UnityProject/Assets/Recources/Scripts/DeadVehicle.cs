using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadVehicle : MonoBehaviour
{

    [SerializeField] private float shrinkSpeed = 0.2f;
    [SerializeField] private float angularVelocityMultiply = 10;
    [SerializeField] private float destroyTime = 3;

    void Start()
    {
        StartCoroutine(DeathTrain());
    }



    IEnumerator DeathTrain()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            Rigidbody childRig = transform.GetChild(i).GetComponent<Rigidbody>();

            Vector3 direction = (transform.forward * Random.Range(1.5f, 3f)) + (transform.right * Random.Range(-0.5f, 0.5f)) + (transform.up * Random.Range(0.5f, 1.5f));
            childRig.AddForce(direction * 4, ForceMode.Impulse);
            childRig.angularVelocity = new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * angularVelocityMultiply;

        }

        yield return new WaitForSeconds(destroyTime);
        
        float childSize = transform.GetChild(0).lossyScale.x;
        while(childSize > 0f)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).localScale = Vector3.one * childSize;
                childSize -= Time.deltaTime * shrinkSpeed;
            }

            yield return new WaitForSeconds(Time.deltaTime);

        }

        Destroy(gameObject);

    }


}
