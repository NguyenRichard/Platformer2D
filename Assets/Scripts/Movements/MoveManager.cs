using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveManager : MonoBehaviour
{
    private PhysicsHandler _physicsHandler;

    public delegate void JumpAction();
    public static event JumpAction OnJump;

    private float jump_impulsion_speed;
    private float max_horizontal_speed;
    public float Max_horizontal_speed
    {
        get { return max_horizontal_speed; }
    }

    private GroundDetection groundDetector;

    private float coyoteTimeDoubleJump;
    
    private bool isJumping = false;

    public bool IsJumping
    {
        get { return this.isJumping; }

        set { this.isJumping = value; }
    }

    [SerializeField]
    private Color noJumpColor;

    [SerializeField]
    private Color oneJumpColor;

    [SerializeField]
    private Color doubleJumpColor;

    [SerializeField]
    private SpriteRenderer sprite;

    private int jump_count = 0;
    public int Jump_count
    {
        get { return this.jump_count; }
        set
        {
            switch (value)
            {
                case 0:
                    sprite.color = doubleJumpColor;
                    break;
                case 1:
                    sprite.color = oneJumpColor;
                    break;
                case 2:
                    sprite.color = noJumpColor;
                    break;
            }

            this.jump_count = value;
        }
    }

    private Vector2 speed = new Vector2(0,0);
    public Vector2 Speed
    {
        get { return this.speed; }

        set { this.speed = value; }
    }

    private void Awake()
    {
        UpdateParameters();
        _physicsHandler = GetComponent<PhysicsHandler>();
        Debug.Assert(_physicsHandler, "You must add a PhysicsHandler !");
        groundDetector = GetComponentInChildren<GroundDetection>();
        Debug.Assert(groundDetector, "You must add a GroundDetector !");
    }

    private void OnEnable()
    {
        GroundDetection.OnLand += OnLandGround;
        GroundDetection.OnLeaveGround += OnLeaveSurface;

        WallDetection.OnWallEncounter += OnWallEncounter;
        WallDetection.OnLeaveWall += OnLeaveSurface;

        ControlParameters.OnUpdatedParam += UpdateParameters;

    }

    private void OnDisable()
    {
        GroundDetection.OnLand -= OnLandGround;
        GroundDetection.OnLeaveGround -= OnLeaveSurface;

        WallDetection.OnWallEncounter -= OnWallEncounter;
        WallDetection.OnLeaveWall -= OnLeaveSurface;

        ControlParameters.OnUpdatedParam -= UpdateParameters;
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
        speed[0] = max_horizontal_speed * airControlFactor * speedRatio;
        _physicsHandler.HorizontalSpeed = speed.x;
    }

    public void Jump()
    {
        if (CanJump())
        {
            return;
        }
        speed.y = jump_impulsion_speed;
        _physicsHandler.VerticalSpeed = speed.y;
        Jump_count++;
        isJumping = true;
        OnJump?.Invoke();
    }

    public void CancelJump()
    {
        if(_physicsHandler.VerticalSpeed > 0)
        {
            speed.y = _physicsHandler.VerticalSpeed/2;
            _physicsHandler.VerticalSpeed = speed.y;
        }
    }

    private bool CanJump()
    {
        return Jump_count >= 2;
    }

    private void OnLandGround() {
        Jump_count = 0;
        isJumping = false;
    }

    private void OnLeaveSurface()
    {
        if (groundDetector.IsGrounded)
        {
            return;
        }
        StartCoroutine(DoubleJumPCoyoteTime());
    }

    IEnumerator DoubleJumPCoyoteTime()
    {
        yield return new WaitForSeconds(0.1f);
        if (!isJumping)
        {
            Jump_count = 1;
        }
    }

    private void OnWallEncounter()
    {
        if (groundDetector.IsGrounded)
        {
            return;
        }
        Jump_count = 1;
        isJumping = false;
    }

    private void UpdateParameters()
    {
        jump_impulsion_speed = ControlParameters.Instance.JumpImpulsionSpeed;
        max_horizontal_speed = ControlParameters.Instance.MaxHorizontalSpeed;
        coyoteTimeDoubleJump = ControlParameters.Instance.CoyoteTimeDoubleJump; 
    }

}
