using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillationx : MonoBehaviour
{

    private enum OscillationMode
    {
        HorizontalOscillation,
        DiagonalOscillation
    }

    [SerializeField] private OscillationMode oscillationMode;
    
    [SerializeField] private float amplitude = 1;
    [SerializeField] private float period = 1;
    
    Vector3 initialPosition;
    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        CallOscillationMode();
    }

    private void CallOscillationMode()
    {
        if (oscillationMode == OscillationMode.HorizontalOscillation)
        {
            float x = amplitude * Mathf.Sin(5f * Time.time / period) + Mathf.Cos(Time.time / period) + Mathf.Sin(Time.time / period);
            transform.position = initialPosition + new Vector3(x, 0, 0);
        }
        
        else if (oscillationMode == OscillationMode.DiagonalOscillation)
        {
            float x = amplitude * Mathf.Sin(2f * Mathf.PI * (Time.time / period));
            transform.position=initialPosition + new Vector3(x,x,0);
        }
    }
}
