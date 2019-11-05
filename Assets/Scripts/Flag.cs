using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{

    public delegate void Victory();
    public static event Victory OnVictory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Flag !");
        OnVictory?.Invoke();
    }
}
