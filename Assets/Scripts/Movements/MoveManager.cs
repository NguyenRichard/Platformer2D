using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField]
    private float final_horizontal_speed; //in physic

    [SerializeField]
    private float jump_speed; //in physic

    [SerializeField]
    private float jump_duration; //in physic

    [SerializeField]
    private GroundDetection groundDetector;
    
    private bool isJumping = true;
    public bool IsJumping
    {
        get { return this.isJumping; }

        set { this.isJumping = value; }
    }

    private int jump_count = 0;

    private Vector2 speed = new Vector2(0,0);
    public Vector2 Speed
    {
        get { return this.speed; }

        set { this.speed = value; }
    }

    public void UpdateHorizontalSpeed(float speedRatio)
    {
        if (!groundDetector.IsGrounded)
        {
            return;
        }
        if(speedRatio < -1 || speedRatio > 1)
        {
            Debug.Log("The input of UpdateSpeed must between -1 and 1.");
            return;
        }
        speed[0] = final_horizontal_speed * speedRatio;
    }

    public void Jump()
    {
        if (!groundDetector.IsGrounded)
        {
            return;
        }

        StartCoroutine(JumpLoop());
    }

    IEnumerator JumpLoop()
    {
        float jumpEnd = Time.time + jump_duration;
        isJumping = true;
        while(Time.time < jumpEnd && isJumping)
        {
            speed = new Vector2(speed.x, jump_speed);
            yield return null;
        }
        speed.y = 0;
        isJumping = false;
    }

    private void Update()
    {
        if (transform.position.y > 0.01)
        {
            Speed += new Vector2(0, -transform.position.y*jump_speed);
        }
        //Update physicHandler;
        transform.position = transform.position + new Vector3(Speed[0] * Time.fixedDeltaTime, Speed[1] * Time.fixedDeltaTime, transform.position[2]); //in physic
        if(transform.position.y < 0.01)
        {
            Speed = new Vector2(Speed[0], 0);
        }
    }
}
