﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetection : MonoBehaviour
{
    [SerializeField]
    private float _height;

    [SerializeField]
    private float _epsilonWidth;

    public delegate void WallEncounterAction();
    public static event WallEncounterAction OnWallEncounter;

    public delegate void LeaveWallGroundAction();
    public static event LeaveWallGroundAction OnLeaveWall;


    private void Awake()
    {
        var playerCollider = GetComponentInParent<BoxCollider2D>();
        var boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
        boxCollider2D.size = new Vector2(playerCollider.size.x + 2*_height + 2*_epsilonWidth, playerCollider.size.y - _epsilonWidth);
        boxCollider2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnWallEncounter?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnLeaveWall?.Invoke();
    }
}
