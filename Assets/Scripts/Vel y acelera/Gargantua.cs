using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gargantua : MonoBehaviour
{
    private MyVector position;
    private MyVector acceleration;
    [SerializeField] private MyVector velocity;
    [Header("World")]

    [SerializeField] Camera camara;
    [SerializeField] private Transform gargantua;

    private void Start()
    {
        position = new MyVector(transform.position.x, transform.position.y);
    }

    private void FixedUpdate()
    {
        Move ();
    }
    
    private void Update()
    {
        position.Draw(Color.blue);
        velocity.Draw(position, Color.red);
        acceleration.Draw(position, Color.green);

        MyVector MyPosition = new MyVector(transform.position.x, transform.position.y);
        MyVector GargantuaPosition = new MyVector(gargantua.position.x, gargantua.position.y);
        
        acceleration = GargantuaPosition - MyPosition;
    }
    private void Move()
    {
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        position = position + velocity * Time.fixedDeltaTime;
        transform.position = new Vector3(position.x, position.y);
    }
}
