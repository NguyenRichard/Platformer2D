using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlParameters : MonoBehaviour
{
    private static ControlParameters instance;

    public static ControlParameters Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ControlParameters();
            }
            return instance;
        }
    }

    [SerializeField]
    private float groundDetection_height; 
    public float GroundDetection_height
    {
        get
        {
            return groundDetection_height;
        }
    }
    [SerializeField]
    private float groundDetection_epsilonWidth;
    public float GroundDetection_epsilonWidth
    {
        get
        {
            return groundDetection_epsilonWidth;
        }
    }
    [SerializeField]
    private float wallDetection_height;
    public float WallDetection_height
    {
        get
        {
            return wallDetection_height;
        }
    }
    [SerializeField]
    private float wallDetection_epsilonWidth;
    public float WallDetection_epsilonWidth
    {
        get
        {
            return wallDetection_epsilonWidth;
        }
    }
}
