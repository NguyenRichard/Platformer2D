using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    private PhysicsHandler _physicsHandler;

    [SerializeField]
    private float jump_speed = 5;
    [SerializeField]
    private float final_horizontal_speed = 3;

    [SerializeField]
    private GroundDetection groundDetector;
    
    private bool isJumping = true;

    public bool IsJumping
    {
        get { return this.isJumping; }

        set { this.isJumping = value; }
    }

    private int jump_count = 0;

    private Vector2 speed = new Vector2(0,0);
    public Vector2 Speed
    {
        get { return this.speed; }

        set { this.speed = value; }
    }

    private void Awake()
    {
        _physicsHandler = GetComponent<PhysicsHandler>();
        Debug.Assert(_physicsHandler, "You must add a PhysicsHandler !");
    }

    public void UpdateHorizontalSpeed(float speedRatio)
    {
        if (!groundDetector.IsGrounded)
        {
            return;
        }
        if(speedRatio < -1 || speedRatio > 1)
        {
            Debug.Log("The input of UpdateSpeed must between -1 and 1.");
            return;
        }
        speed[0] = final_horizontal_speed * speedRatio;
        _physicsHandler.horizontalSpeed = speed.x;
    }

    public void Jump()
    {
        if (!groundDetector.IsGrounded)
        {
            return;
        }
        speed.y = jump_speed;
        _physicsHandler.verticalSpeed = speed.y;
    }

    private void Update()
    {
    }
}
