using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PolarExperiments : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] float angleDeg;
    
    [Header ("Speed and Acceleration")]
    [SerializeField] float angularSpeed;
    [SerializeField] float angularAccele;
    [SerializeField] float radialSpeed;
    [SerializeField] float radialAccele;

    [Header("World")]
    [SerializeField] Transform bolita;

    private void Start()
    {
        Assert.IsNotNull(bolita, "Bolita Reference is required ");
    }

    private void Update()
    {
        //Incrementar ángulo y rad
        angleDeg += angularSpeed * Time.deltaTime;
        radius += radialSpeed * Time.deltaTime;

        //Update posición bolita
        bolita.position = PolarToCartesian(radius, angleDeg);
        Debug.DrawLine(Vector3.zero, bolita.position, Color.red);
    }
    private Vector3 PolarToCartesian(float radius, float angle)
    {
        float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector3(x, y, 0);
    }
}