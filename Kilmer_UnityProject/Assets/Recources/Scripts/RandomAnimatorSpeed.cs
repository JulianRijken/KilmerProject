using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimatorSpeed : MonoBehaviour
{

    private Animator animatior;

    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float minSpeed = 0;
    [SerializeField] private float timeBitweenChange = 0.1f;

    [Header("Use for smooth change")]
    [SerializeField] private bool lerp = false;
    [SerializeField] private float lerpSpeed = 0.1f;

    private float timer = 0;

    void Start()
    {
        animatior = GetComponent<Animator>();
    }

    void Update()
    {
        float setSpeed = 0;


        if (timeBitweenChange == 0)
        {
            setSpeed = Random.Range(minSpeed, maxSpeed);
        }
        else
        {
            timer += Time.deltaTime;
            
            if(timer >= timeBitweenChange)
            {
                setSpeed = Random.Range(minSpeed, maxSpeed);
                timer = 0;
            }
        }

        if(lerp)
        {
            animatior.speed = Mathf.Lerp(animatior.speed, setSpeed, Time.deltaTime / lerpSpeed);
        }
        else
        {
            animatior.speed = setSpeed;
        }


    }
}
