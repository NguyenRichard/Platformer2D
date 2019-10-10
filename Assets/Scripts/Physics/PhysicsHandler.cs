using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsHandler : MonoBehaviour
{
    private CollisionHandler _collisionHandler;
    
    [SerializeField]
    private float maxSpeed = 5;

    [SerializeField]
    private float horizontalSpeed;
    [SerializeField]
    private float verticalSpeed;
    [SerializeField]
    private float verticalAcceleration;

    private void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        Debug.Assert(_collisionHandler != null);
        horizontalSpeed = 0;
        verticalAcceleration = 0;
        verticalSpeed = 0;
    }

    void Update()
    {
        float horizontalTrajectory = horizontalSpeed*Time.fixedDeltaTime;
        verticalSpeed = verticalAcceleration * Time.fixedDeltaTime + verticalSpeed;
        float verticalTrajectory = verticalSpeed * Time.fixedDeltaTime;
        Debug.DrawRay(transform.position, new Vector2(horizontalTrajectory,0), Color.red);
        if (_collisionHandler.CorrectHorizontalMovement(ref horizontalTrajectory))
        {
            horizontalSpeed = 0;
        }
        if (_collisionHandler.CorrectVerticalMovement(ref verticalTrajectory))
        {
            verticalAcceleration = 0;
            verticalSpeed = 0;
        }
        Debug.DrawRay(transform.position, new Vector2(horizontalTrajectory, 0), Color.blue);
        Vector2 position = transform.position;
        position += new Vector2(horizontalTrajectory, verticalTrajectory);
        transform.position = position;
    }
}
