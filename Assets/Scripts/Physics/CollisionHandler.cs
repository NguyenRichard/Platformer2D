using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private Vector2[] castHorizontalPoints;
    private LayerMask terrainMask;
    [SerializeField]
    private float epsilon = 0.05f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Debug.Assert(_rigidbody2D != null, "You must add a Rigidbody2D to the object");
        Debug.Assert(_rigidbody2D.isKinematic, "The Rigidbody2D must be Kinematic !");
        terrainMask = LayerMask.GetMask("Terrain");
    }
    
    public bool CorrectHorizontalMovement(float horizontalTrajectory)
    
    {
        Vector2 position = transform.position;
        bool hasChanged = false;

        for (int i = 0; i < castHorizontalPoints.Length; i++)
        {
            var cast = Physics2D.Raycast(position + castHorizontalPoints[i], new Vector2(horizontalTrajectory, 0),
                Math.Abs(horizontalTrajectory), terrainMask);
            if (cast.collider)
            {
                if (cast.distance < horizontalTrajectory)
                {
                    horizontalTrajectory = cast.distance;
                    hasChanged = true;
                }
            }
        }
        return hasChanged;
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 position = transform.position;
        for (int i = 0; i < castHorizontalPoints.Length; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(position+castHorizontalPoints[i], new Vector3(0.1f,0.1f));
        }
    }
}
