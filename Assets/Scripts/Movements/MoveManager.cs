﻿using System.Collections;
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

    private bool isGrounded = true; //in physic

    private int jump_count = 0;

    private Vector2 speed = new Vector2(0,0);
    public Vector2 Speed
    {
        get { return this.speed; }

        set { this.speed = value; }
    }

    public void UpdateHorizontalSpeed(float speedRatio)
    {
        if (!isGrounded)
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
        if (!isGrounded)
        {
            return;
        }

        StartCoroutine(JumpLoop());
        isGrounded = false; //in physic
    }

    IEnumerator JumpLoop()
    {
        float jumpEnd = Time.time + jump_duration;
        while(Time.time < jumpEnd)
        {
            speed = new Vector2(speed.x, jump_speed);
            yield return null;
        }
        speed.y = 0;
    }

    private void Update()
    {
        //Update physicHandler;
        transform.position = transform.position + new Vector3(Speed[0] * Time.fixedDeltaTime, Speed[1] * Time.fixedDeltaTime, transform.position[2]); //in physic
    }
}
