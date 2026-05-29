using System;
using UnityEngine;

public class BoostBehavior : MonoBehaviour
{
    public bool alreadyConsumed = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(transform.position.x) > 30 || Math.Abs(transform.position.z) > 30)
        {
            Destroy(gameObject);
        }
    }
}
