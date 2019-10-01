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
    private Vector2 _speed;
    public Vector2 Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
            if (_speed.magnitude > maxSpeed)
            {
                _speed = value.normalized;
                var sqr = Mathf.Sqrt(maxSpeed);
                _speed.Scale(new Vector2(sqr, sqr));
            }
        }
    }

    private void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        Debug.Assert(_collisionHandler != null);
    }

    void Update()
    {
        Vector2 trajectory = Speed*Time.fixedDeltaTime;
        if (_collisionHandler.CorrectMovement(ref trajectory))
        {
            Speed = Vector2.zero;
        }
        Vector2 position = transform.position;
        position += trajectory;
        transform.position = position;
    }
}
