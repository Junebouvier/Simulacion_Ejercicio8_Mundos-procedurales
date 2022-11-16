using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBolita : MonoBehaviour
{
    private MyVector position;
    private MyVector displacement;
    [SerializeField] private MyVector velocity;
    [SerializeField] private MyVector acceleration;
    [Range(0f, 1f)] [SerializeField] private float dampingFactor = 0.9f;

    [Header("World")] [SerializeField] Camera camera;

    private int currentAccelerationCounter = 0;

    private readonly MyVector[] directions = new MyVector[4]
    {
        new MyVector(0, -9.8f),
        new MyVector(9.8f, 0f),
        new MyVector(0, 9.8f),
        new MyVector(-9.8f, 0f)
    };

    private void Start()
    {
        position = new MyVector(transform.position.x, transform.position.y);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        position.Draw(Color.blue);
        displacement.Draw(position, Color.red);
        acceleration.Draw(position, Color.green);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Change the accel
            acceleration = directions[(++currentAccelerationCounter) % directions.Length];
            velocity *= 0;
        }

    }

    private void Move()
    {
        //Euler integrator
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        position = position + velocity * Time.fixedDeltaTime;

        //Check horizontal bounds
        if (position.x > camera.orthographicSize) //CheckRight
        {
            velocity.x *= -1;
            position.x = camera.orthographicSize;
            velocity *= dampingFactor; //Damping
        }
        else if (position.x < -camera.orthographicSize) //CheckRight
        {
            velocity.x *= -1;
            position.x = -camera.orthographicSize;
            velocity *= dampingFactor; //Damping

        }

        //Check vertical bounds
        if (position.y > camera.orthographicSize) //CheckUp
        {
            velocity.y *= -1;
            position.y = camera.orthographicSize;
            velocity *= dampingFactor; //Damping
        }
        else if (position.y < -camera.orthographicSize) //CheckDown
        {
            velocity.y *= -1;
            position.y = -camera.orthographicSize;
            velocity *= dampingFactor; //Damping
        }

        transform.position = new Vector3(position.x, position.y);
    }
}
