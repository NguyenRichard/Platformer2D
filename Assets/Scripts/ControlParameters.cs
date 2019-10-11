using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ControlParameters : MonoBehaviour
{
    private static ControlParameters instance;

    public static ControlParameters Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is several instances of ControlParameters in the scene !");
            return;
        }

        instance = this;
    }

    [SerializeField]
    private float groundDetection_height; 
    public float GroundDetection_height
    {
        get
        {
            return groundDetection_height;
        }
    }
    [SerializeField]
    private float groundDetection_epsilonWidth;
    public float GroundDetection_epsilonWidth
    {
        get
        {
            return groundDetection_epsilonWidth;
        }
    }
    [SerializeField]
    private float wallDetection_height;
    public float WallDetection_height
    {
        get
        {
            return wallDetection_height;
        }
    }
    [SerializeField]
    private float wallDetection_epsilonWidth;
    public float WallDetection_epsilonWidth
    {
        get
        {
            return wallDetection_epsilonWidth;
        }
    }

    [SerializeField]
    private float jumpImpulsionSpeed;
    public float JumpImpulsionSpeed
    {
        get
        {
            return jumpImpulsionSpeed;
        }
    }

    [SerializeField]
    private float maxHorizontalSpeed;
    public float MaxHorizontalSpeed
    {
        get
        {
            return maxHorizontalSpeed;
        }
    }

    [SerializeField]
    private float coyoteTimeDoubleJump;
    public float CoyoteTimeDoubleJump
    {
        get
        {
            return coyoteTimeDoubleJump;
        }
    }
}
