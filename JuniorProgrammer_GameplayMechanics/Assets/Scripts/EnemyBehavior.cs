using System;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 1.0f;
    public float minHeight = -5.0f;
    
    private Vector3 _lookDirection;
    
    private Rigidbody _rigidbody;
    private GameObject _player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _lookDirection = (_player.transform.position - transform.position).normalized;
        
        _rigidbody.AddForce(_lookDirection * speed);
        
        if (transform.position.y < minHeight)
        {
            Destroy(gameObject);
        }
    }
}
