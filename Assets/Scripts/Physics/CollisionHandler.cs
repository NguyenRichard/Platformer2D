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
    [SerializeField]
    private float epsilon = 0.05f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Debug.Assert(_rigidbody2D != null, "You must add a Rigidbody2D to the object");
        Debug.Assert(_rigidbody2D.isKinematic, "The Rigidbody2D must be Kinematic !");
        terrainMask = LayerMask.GetMask("Terrain");
    }
    
    public bool CorrectMovement(ref Vector2 trajectory)
    
    {
        Vector2 position = transform.position;
        float min_distance = trajectory.magnitude;
        for (int i = 0; i < castPoints.Length; i++)
        {
            var cast = Physics2D.Raycast(position+castPoints[i], trajectory, trajectory.magnitude, terrainMask);
            Debug.DrawRay(position+castPoints[i], trajectory, Color.red);
            if (cast.collider)
            {
                if (cast.distance < min_distance)
                    min_distance = cast.distance;
            }
        }

        if (min_distance < trajectory.magnitude)
        {
            trajectory.Normalize();
            var sqr = Mathf.Sqrt(min_distance);
            trajectory.Scale(new Vector2(sqr-epsilon, sqr-epsilon));
            return true;
        }

        return false;
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
