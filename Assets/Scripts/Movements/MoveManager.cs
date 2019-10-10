using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    private void OnEnable()
    {
        GroundDetection.OnLand += OnLand;
    }

    private void OnDisable()
    {
        GroundDetection.OnLand -= OnLand;
    }

    public void UpdateHorizontalSpeed(float speedRatio)
    {
        if (speedRatio < -1 || speedRatio > 1)
        {
            Debug.Log("The input of UpdateSpeed must between -1 and 1.");
            return;
        }
        float airControlFactor = 1;
        if (!groundDetector.IsGrounded)
        {
            airControlFactor = 0.5f;
        }
        speed[0] = final_horizontal_speed * airControlFactor * speedRatio;
        _physicsHandler.horizontalSpeed = speed.x;
    }

    public void Jump()
    {
        if (CanJump())
        {
            return;
        }
        speed.y = jump_speed;
        _physicsHandler.verticalSpeed = speed.y;
        jump_count++;
    }

    public void CancelJump()
    {
        if(_physicsHandler.verticalSpeed > 0)
        {
            speed.y = _physicsHandler.verticalSpeed/2;
            _physicsHandler.verticalSpeed = speed.y;
        }
    }

    private bool CanJump()
    {
        return jump_count >= 2;
    }

    private void OnLand() {
        jump_count = 0;
    }
}
