using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsHandler : MonoBehaviour
{
    private CollisionHandler _collisionHandler;
    private GroundDetection _groundDetection;
    
    [SerializeField]
    private float maxSpeed = 5;

    [SerializeField]
    public float horizontalSpeed;
    [SerializeField]
    public float verticalSpeed;
    [SerializeField]
    private float verticalAcceleration;

    private void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        _groundDetection = GetComponentInChildren<GroundDetection>();
        Debug.Assert(_collisionHandler != null);
        Debug.Assert(_groundDetection != null);
        horizontalSpeed = 0;
        verticalAcceleration = 0;
        verticalSpeed = 0;
    }

    void Update()
    {
        float horizontalTrajectory = horizontalSpeed*Time.fixedDeltaTime;
        verticalSpeed = verticalAcceleration * Time.fixedDeltaTime + verticalSpeed;
        if (!_groundDetection.IsGrounded)
        {
            verticalSpeed += Physics2D.gravity.y * Time.deltaTime;
        }
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
