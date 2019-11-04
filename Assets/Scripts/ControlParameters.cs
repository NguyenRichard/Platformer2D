using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ControlParameters : MonoBehaviour
{
    private static ControlParameters instance;

    public delegate void ParametersUpdate();
    public static event ParametersUpdate OnUpdatedParam;

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
        set
        {
            groundDetection_height = value;
            OnUpdatedParam?.Invoke();
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
        set
        {
            groundDetection_epsilonWidth = value;
            OnUpdatedParam?.Invoke();
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
        set
        {
            wallDetection_height = value;
            OnUpdatedParam?.Invoke();
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
        set
        {
            wallDetection_epsilonWidth = value;
            OnUpdatedParam?.Invoke();
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
        set
        {
            jumpImpulsionSpeed = value;
            OnUpdatedParam?.Invoke();
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
        set
        {
            maxHorizontalSpeed = value;
            OnUpdatedParam?.Invoke();
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
        set
        {
            coyoteTimeDoubleJump = value;
            OnUpdatedParam?.Invoke();
        }
    }

    [SerializeField]
    private float collisionEpsilon;
    public float CollisionEpsilon
    {
        get
        {
            return collisionEpsilon;
        }
        set
        {
            collisionEpsilon = value;
            OnUpdatedParam?.Invoke();
        }
    }

    [SerializeField]
    private float descendingGravityModifier;
    public float DescendingGravityModifier
    {
        get
        {
            return descendingGravityModifier;
        }
        set
        {
            descendingGravityModifier = value;
            OnUpdatedParam?.Invoke();
        }
    }

    private void OnValidate()
    {
        OnUpdatedParam?.Invoke();
    }
}
