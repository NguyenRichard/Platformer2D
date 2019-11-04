using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Flag !");
        LevelManager levelManager = GetComponentInParent<LevelManager>();
        levelManager.LoadNextScene();
    }
}
