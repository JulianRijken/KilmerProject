using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    [SerializeField] private int[] mask = null;
    [SerializeField] private GameObject destroyedObject = null;


    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < mask.Length; i++)
        {
            if (collision.gameObject.layer == mask[i])
            {
                if(destroyedObject != null)
                    Instantiate(destroyedObject, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
