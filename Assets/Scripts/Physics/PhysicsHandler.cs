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
    public float horizontalSpeed = 0;
    [SerializeField]
    public float verticalSpeed = 0;
    [SerializeField]
    private float verticalAcceleration = 0;
    [SerializeField]
    private float descendingGravityModifier = 2f;

    private void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        _groundDetection = GetComponentInChildren<GroundDetection>();
        Debug.Assert(_collisionHandler != null);
        Debug.Assert(_groundDetection != null);
    }

    void Update()
    {
        float horizontalTrajectory = horizontalSpeed*Time.fixedDeltaTime;
        verticalSpeed = verticalAcceleration * Time.fixedDeltaTime + verticalSpeed;
        if (!_groundDetection.IsGrounded)
        {
            verticalSpeed += (verticalSpeed < 0 ? 2f : 1f)*Physics2D.gravity.y * Time.deltaTime;
        }
        float verticalTrajectory = verticalSpeed * Time.fixedDeltaTime;
        if (_collisionHandler.CorrectHorizontalMovement(ref horizontalTrajectory))
        {
            horizontalSpeed = 0;
        }
        if (_collisionHandler.CorrectVerticalMovement(ref verticalTrajectory))
        {
            verticalAcceleration = 0;
            verticalSpeed = 0;
        }
        Vector2 position = transform.position;
        position += new Vector2(horizontalTrajectory, verticalTrajectory);
        transform.position = position;
    }
}
