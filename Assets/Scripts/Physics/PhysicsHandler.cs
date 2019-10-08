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

    private void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        Debug.Assert(_collisionHandler != null);
    }

    void Update()
    {
        float horizontalTrajectory = horizontalSpeed*Time.fixedDeltaTime;
        Debug.DrawRay(transform.position, new Vector2(horizontalTrajectory,0), Color.red);
        if (_collisionHandler.CorrectHorizontalMovement(horizontalTrajectory))
        {
            horizontalSpeed = 0;
        }
        Debug.DrawRay(transform.position, new Vector2(horizontalTrajectory, 0), Color.blue);
        Vector2 position = transform.position;
        position += new Vector2(horizontalTrajectory, 0);
        transform.position = position;
    }
}
