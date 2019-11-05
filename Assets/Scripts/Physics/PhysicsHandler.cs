using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PhysicsHandler : MonoBehaviour
{
    private CollisionHandler _collisionHandler;
    private GroundDetection _groundDetection;

    [SerializeField]
    private Vector2 speed = Vector2.zero;
    [SerializeField]
    private Vector2 acceleration = Vector2.zero;
    private float descendingGravityModifier;

    public List<Vector2> environmentAccelerationModifiers;
    public List<Vector2> environmentSpeedModifiers;

    public float HorizontalSpeed
    {
        get => speed.x;
        set => speed.x = value;
    }

    public float VerticalSpeed
    {
        get => speed.y;
        set => speed.y = value;
    }

    private void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        _groundDetection = GetComponentInChildren<GroundDetection>();
        Debug.Assert(_collisionHandler != null);
        Debug.Assert(_groundDetection != null);
        UpdateParameters();
    }

    private void OnEnable()
    {
        ControlParameters.OnUpdatedParam += UpdateParameters;
    }

    private void OnDisable()
    {
        ControlParameters.OnUpdatedParam -= UpdateParameters;
    }

    void Update()
    {
        if (!_groundDetection.IsGrounded)
        {
            acceleration.y = (VerticalSpeed < 0 ? descendingGravityModifier : 1f)*Physics2D.gravity.y;
            for (int i = 0; i < environmentAccelerationModifiers.Count; i++)
            {
                acceleration += environmentAccelerationModifiers[i];
            }
        }
        speed += acceleration * Time.deltaTime;
        for (int i = 0; i < environmentSpeedModifiers.Count; i++)
        {
            speed += environmentSpeedModifiers[i];
        }
        float horizontalTrajectory = HorizontalSpeed*Time.deltaTime;
        float verticalTrajectory = VerticalSpeed*Time.deltaTime;
        if (_collisionHandler.CorrectHorizontalMovement(ref horizontalTrajectory))
        {
            acceleration.x = 0;
            HorizontalSpeed = 0;
        }
        if (_collisionHandler.CorrectVerticalMovement(ref verticalTrajectory))
        {
            acceleration.y = 0;
            VerticalSpeed = 0;
        }
        Vector2 position = transform.position;
        position += new Vector2(horizontalTrajectory, verticalTrajectory);
        transform.position = position;
    }

    private void UpdateParameters()
    {
        descendingGravityModifier = ControlParameters.Instance.DescendingGravityModifier;
    }
}
