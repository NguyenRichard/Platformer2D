using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private LayerMask _terrainMask;
    private LayerMask _platformAndTerrainMask;
    private LayerMask _platformMask;
    [SerializeField]
    private float epsilon = 0.01f;

    private bool _isInPlatform = false;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        Debug.Assert(_rigidbody2D != null, "You must add a Rigidbody2D to the object");
        Debug.Assert(_rigidbody2D.isKinematic, "The Rigidbody2D must be Kinematic !");
        _terrainMask = LayerMask.GetMask("Ground");
        _platformAndTerrainMask = LayerMask.GetMask("Ground", "Platform");
        _platformMask = LayerMask.GetMask("Platform");
    }
    
    public bool CorrectHorizontalMovement(ref float horizontalTrajectory)
    {
        Vector2 position = transform.position;

        _isInPlatform = Physics2D.BoxCast(position, _boxCollider2D.size, 0, Vector2.zero, 0, _platformMask).collider;
        var boxCast = Physics2D.BoxCast(position, _boxCollider2D.size, 0, new Vector2(horizontalTrajectory, 0),
            Math.Abs(horizontalTrajectory), (_isInPlatform ? _terrainMask : _platformAndTerrainMask));
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
            Math.Abs(verticalTrajectory), (verticalTrajectory > 0 ? _terrainMask : _platformAndTerrainMask));
        if (boxCast.collider && boxCast.distance < Math.Abs(verticalTrajectory) && (!_isInPlatform))
        {
            verticalTrajectory = Math.Sign(verticalTrajectory) * (boxCast.distance-epsilon);
            return true;
        }
        return false;
    }
}
