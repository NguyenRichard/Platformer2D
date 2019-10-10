using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField]
    private float _height;
    [SerializeField]
    private float _epsilonWidth;
    private bool isGrounded = false;
    public bool IsGrounded
    {
        get { return isGrounded;  }
    }

    private void Awake()
    {
        var playerCollider = GetComponentInParent<BoxCollider2D>();
        var boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
        boxCollider2D.size = new Vector2(playerCollider.size.x-_epsilonWidth, _height);
        boxCollider2D.offset = new Vector2(0, -playerCollider.size.y/2-_epsilonWidth/2);
        boxCollider2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Land !");
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Jump !");
        isGrounded = false;
    }

}
