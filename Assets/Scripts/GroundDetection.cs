﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    private float _height;

    private float _epsilonWidth;
    private bool isGrounded = false;
    public bool IsGrounded
    {
        get { return isGrounded; }
    }

    public delegate void LandAction();
    public static event LandAction OnLand;

    public delegate void LeaveGroundAction();
    public static event LeaveGroundAction OnLeaveGround;


    private void Awake()
    {
        var playerCollider = GetComponentInParent<BoxCollider2D>();
        var boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
        _height = ControlParameters.Instance.GroundDetection_height;
        _epsilonWidth = ControlParameters.Instance.GroundDetection_epsilonWidth;
        boxCollider2D.size = new Vector2(playerCollider.size.x-_epsilonWidth, _height);
        boxCollider2D.offset = new Vector2(0, -playerCollider.size.y/2-_epsilonWidth/2);
        boxCollider2D.isTrigger = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        OnLand?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
        OnLeaveGround?.Invoke();
    }

}
