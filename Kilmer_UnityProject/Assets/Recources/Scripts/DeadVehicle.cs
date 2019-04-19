using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadVehicle : MonoBehaviour
{

    [SerializeField] private float shrinkSpeed = 0.2f;
    [SerializeField] private float angularVelocity = 10;
    [SerializeField] private float extraUpwardsVelocity = 3;
    [SerializeField] private Vector3 velocity = new Vector3(0,0,0);
    [SerializeField] private float destroyTime = 3;

    IEnumerator Start()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            Rigidbody childRig = transform.GetChild(i).GetComponent<Rigidbody>();

            if (childRig != null)
            {
                childRig.AddForce(new Vector3(velocity.x,velocity.y + extraUpwardsVelocity, velocity.z), ForceMode.Impulse);
                childRig.angularVelocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * angularVelocity;
            }
        }

        yield return new WaitForSeconds(destroyTime);

        for (int i = 0; i < transform.childCount; i++)
        {
            Shrink(transform.GetChild(i), shrinkSpeed);
        }


        yield return new WaitUntil(() => transform.childCount == 0);
        Destroy(gameObject);

    }

    public void SetVelocity(Vector3 _velocity)
    {
        velocity = _velocity;
    }


    private void Shrink(Transform _transform, float _speed)
    {
        StartCoroutine(IShrink(_transform, _speed));
    }
    IEnumerator IShrink(Transform _transform, float _speed)
    {
        while (_transform.lossyScale.magnitude > 0)
        {
            Vector3 toScale = _transform.localScale;
            toScale -= Vector3.one * Time.deltaTime * _speed;
            toScale = new Vector3(Mathf.Clamp(toScale.x, 0, Mathf.Infinity), Mathf.Clamp(toScale.y, 0, Mathf.Infinity), Mathf.Clamp(toScale.z, 0, Mathf.Infinity));
            _transform.localScale = toScale;
            yield return new WaitForEndOfFrame();
        }

        Destroy(_transform.gameObject);
    }

}