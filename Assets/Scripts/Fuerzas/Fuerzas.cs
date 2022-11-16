using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random = System.Random;

public class Fuerzas : MonoBehaviour
{
    public enum BolitaRunMode
    {
        Friction,
        FluidFriction,
        Gravity
    }

    public float Mass => mass;
    
    private MyVector position;
    [SerializeField] private BolitaRunMode runMode;
    [SerializeField] private MyVector velocity;
    [SerializeField] private MyVector acceleration;
    [SerializeField] private float mass = 1f;

    [Header("Forces")]
    [SerializeField] private MyVector wind;
    [SerializeField] private MyVector gravity;
    
    [Header("Other settings")]
    [SerializeField] private Camera camera;

    [SerializeField] private Fuerzas otraBolita;
    [Range(0f, 1f)][SerializeField] private float dampingFactor = 0.9f;
    [Range(0f, 1f)][SerializeField] private float frictionCoefficient = 0.9f;
    private void Start()
    {
        position = new MyVector(transform.position.x, transform.position.y);
    }

    private void FixedUpdate()
    {
        Vector2 v = Vector2.one;
        float magnitud = v.magnitude;
        
        acceleration *= 0f;
        
        //Apply wight if we're not simulating Newton's gravity attraction
        if (runMode != BolitaRunMode.Gravity)
        {
            MyVector weight = gravity * mass;
            ApplyForce(weight);
        }
        
        if (runMode == BolitaRunMode.FluidFriction)
        {
            //Fluid friction
            ApplyFluidFriction();
        }
        else if (runMode == BolitaRunMode.Friction)
        {
            //Friction
            ApplyFriction();
        } 
        
        else if (runMode == BolitaRunMode.Gravity)
        {
            MyVector diff = otraBolita.position - position;
            float distance = diff.magnitude;
            float scalarPart = (mass * otraBolita.mass / (distance * distance));
            MyVector gravity = scalarPart * diff.normalized;
            ApplyForce(gravity);
        }
        
        //Integrate acceleration
        Move();
    }

    private void Update()
    {
        position.Draw(Color.blue);
        velocity.Draw(position, Color.red);
    }

    private void Move()
    {
        //Euler Integrator
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        position = position + velocity * Time.fixedDeltaTime;

        if (runMode == BolitaRunMode.Gravity)
        {
            CheckLimitSpeed();
        }
        
        //World is a box only if we're not simulating gravity attraction
        if (runMode != BolitaRunMode.Gravity)
        {
            CheckWorldBoxBounds();
        }
        
        //Tell Unity what's the new object position
        transform.position = new Vector3(position.x, position.y);
    }
    private void ApplyForce(MyVector force)
    {
        acceleration += force / mass;
    }

    private void ApplyFriction()
    {
        //Friction
        float N = -mass * gravity.y;
        MyVector friction = -frictionCoefficient * N * velocity.normalized;
        ApplyForce(friction);
        friction.Draw(position, Color.blue);
    }

    private void ApplyFluidFriction()
    {
        if (transform.localPosition.y <= 0)
        {
            float frontalArea = transform.localScale.x;
            float densidad = 1;
            float fluidDragCoefficient = 1;
            float velocityMagnitude = velocity.magnitude;
            float scalarPart = -0.5f * densidad * velocityMagnitude * velocityMagnitude * frontalArea *fluidDragCoefficient;
            MyVector friction = scalarPart * velocity.normalized;
            ApplyForce(friction);
        }
    }

    private void CheckLimitSpeed(float maxSpeed = 10)
    {
        if (velocity.magnitude > 10)
        {
            velocity = 10 * velocity.normalized;
        }
    }

    private void CheckWorldBoxBounds()
    {
        
        //Check horizontal bounds
        if (position.x > camera.orthographicSize)
        {
            velocity.x *= -1;
            position.x = camera.orthographicSize;
            velocity *= dampingFactor;
        }
        else if (position.x < -camera.orthographicSize)
        {
            velocity.x *= -1;
            position.x = -camera.orthographicSize;
            velocity *= dampingFactor;
        }          
                
        //Check vertical bounds
        if (position.y > camera.orthographicSize)
        {
            velocity.y *= -1;
            position.y = camera.orthographicSize;
            velocity *= dampingFactor;
        }
        else if (position.y < -camera.orthographicSize)
        {
            velocity.y *= -1;
            position.y = -camera.orthographicSize;
            velocity *= dampingFactor;
        }
    }
    
}