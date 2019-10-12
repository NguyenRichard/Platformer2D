using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Environment : MonoBehaviour
{
    [SerializeField]
    private Vector2 accelerationModifier = Vector2.zero;
    [SerializeField]
    private Vector2 speedModifier = Vector2.zero;

    private PhysicsHandler _physicsHandler;

    private void Awake()
    {
        var collider2D = GetComponent<Collider2D>();
        Debug.Assert(collider2D);
        Debug.Assert(collider2D.isTrigger);
        _physicsHandler = GameObject.FindWithTag("Player").GetComponent<PhysicsHandler>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _physicsHandler.environmentAccelerationModifiers.Append(accelerationModifier);
        _physicsHandler.environmentSpeedModifiers.Append(speedModifier);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _physicsHandler.environmentAccelerationModifiers.Remove(accelerationModifier);
        _physicsHandler.environmentSpeedModifiers.Remove(speedModifier);
    }
}
