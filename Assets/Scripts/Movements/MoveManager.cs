﻿using System;
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

    [SerializeField]
    private float coyoteTimeDoubleJump;
    
    private bool isJumping = false;

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
        GroundDetection.OnLand += OnLandGround;
        GroundDetection.OnLeaveGround += OnLeaveSurface;
        WallDetection.OnWallEncounter += OnWallEncounter;
        WallDetection.OnLeaveWall += OnLeaveSurface;

    }

    private void OnDisable()
    {
        GroundDetection.OnLand -= OnLandGround;
        GroundDetection.OnLeaveGround -= OnLeaveSurface;
        WallDetection.OnWallEncounter -= OnWallEncounter;
        WallDetection.OnLeaveWall -= OnLeaveSurface;
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
        isJumping = true;
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

    private void OnLandGround() {
        jump_count = 0;
        isJumping = false;
    }

    private void OnLeaveSurface()
    {
        StartCoroutine(DoubleJumPCoyoteTime());
    }

    IEnumerator DoubleJumPCoyoteTime()
    {
        yield return new WaitForSeconds(0.1f);
        if (!isJumping)
        {
            jump_count = 1;
        }
    }

    private void OnWallEncounter()
    {
        jump_count = 1;
        isJumping = false;
    }
}
