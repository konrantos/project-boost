using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 moventVector;

    float moventFactor; 
    [SerializeField] float period = 2f;


    void Start()
    {
        startingPosition = transform.position; // curent position

    }

    void Update()
    {
        if (period <= Mathf.Epsilon) {return;} // for NaN Error

        float cycles =Time.time / period ; // growing over time

        const float tau = Mathf.PI *2; // 6.283...

        float rawSinWave = MathF.Sin(cycles*tau); // going from -1 to 1

        moventFactor = (rawSinWave + 1f) /2f ; // recalculated to go from 0 to 1

        Vector3 offset = moventVector * moventFactor;
        transform.position = startingPosition + offset;
    }
}
