using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private LayerMask terrainMask;
    [SerializeField]
    private float epsilon = 0.01f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        Debug.Assert(_rigidbody2D != null, "You must add a Rigidbody2D to the object");
        Debug.Assert(_rigidbody2D.isKinematic, "The Rigidbody2D must be Kinematic !");
        terrainMask = LayerMask.GetMask("Ground");
    }
    
    public bool CorrectHorizontalMovement(ref float horizontalTrajectory)
    {
        Vector2 position = transform.position;

        var boxCast = Physics2D.BoxCast(position, _boxCollider2D.size, 0, new Vector2(horizontalTrajectory, 0),
            Math.Abs(horizontalTrajectory), terrainMask);
        if (boxCast.collider && boxCast.distance < Math.Abs(horizontalTrajectory))
        {
            horizontalTrajectory = Math.Sign(horizontalTrajectory) * (boxCast.distance-epsilon);
            return true;
        }
        return false;
    }
    
    public bool CorrectVerticalMovement(ref float verticalTrajectory)
    {
        Vector2 position = transform.position;

        var boxCast = Physics2D.BoxCast(position, _boxCollider2D.size, 0, new Vector2(0, verticalTrajectory),
            Math.Abs(verticalTrajectory), terrainMask);
        if (boxCast.collider && boxCast.distance < Math.Abs(verticalTrajectory))
        {
            verticalTrajectory = Math.Sign(verticalTrajectory) * (boxCast.distance-epsilon);
            return true;
        }
        return false;
    }
}
