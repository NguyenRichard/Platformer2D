using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    private float start_time;
    [SerializeField]
    private float duration;


    private void OnEnable()
    {
        start_time = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > start_time + duration)
        {
            Destroy(gameObject);
        }
    }
}
