using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private Vector2[] castPoints;
    private LayerMask terrainMask;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Debug.Assert(_rigidbody2D != null, "You must add a Rigidbody2D to the object");
        Debug.Assert(_rigidbody2D.isKinematic, "The Rigidbody2D must be Kinematic !");
        terrainMask = LayerMask.GetMask("Terrain");
    }
    
    public Vector2 CorrectMovement(Vector2 speed)
    {
        Vector2 position = transform.position;
        for (int i = 0; i < castPoints.Length; i++)
        {
            var cast = Physics2D.Raycast(position+castPoints[i], speed, speed.magnitude, terrainMask);
            Debug.DrawRay(position+castPoints[i], speed, Color.blue);
            if (cast.collider)
            {
                speed.Normalize();
                var sqr = Mathf.Sqrt(cast.distance);
                speed.Scale(new Vector2(sqr, sqr));
            }
        }
        return speed;
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 position = transform.position;
        for (int i = 0; i < castPoints.Length; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(position+castPoints[i], new Vector3(0.1f,0.1f));
        }
    }
}
