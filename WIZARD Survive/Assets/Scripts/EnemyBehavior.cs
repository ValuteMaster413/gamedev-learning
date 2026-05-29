using System;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 10f;
    public bool alreadyHit =  false;
    
    private Vector3 _lookDirection;
    private Rigidbody _rigidbody;
    private GameObject _player;
    private SpawnManager _spawnManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _lookDirection = (_player.transform.position - transform.position).normalized;
        
        _rigidbody.linearVelocity = new Vector3(_lookDirection.x * speed, _rigidbody.linearVelocity.y, _lookDirection.z * speed);
        
        Quaternion targetRotation = Quaternion.LookRotation(_lookDirection);
        _rigidbody.rotation = Quaternion.Slerp(
            _rigidbody.rotation,
            targetRotation,
            15f * Time.fixedDeltaTime
        );
        
        if (Math.Abs(transform.position.x) > 30 || Math.Abs(transform.position.z) > 30)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("GreenSpell") || collision.gameObject.CompareTag("UberSpell")) && transform.gameObject.CompareTag("GreenEnemy"))
        {
            Destroy(gameObject);
            _spawnManager.UpdateScore(5);
        }
        
        if ((collision.gameObject.CompareTag("BlueSpell") || collision.gameObject.CompareTag("UberSpell")) && transform.gameObject.CompareTag("BlueEnemy"))
        {
            Destroy(gameObject);
            _spawnManager.UpdateScore(5);
        }
        
        if ((collision.gameObject.CompareTag("RedSpell") || collision.gameObject.CompareTag("UberSpell")) && transform.gameObject.CompareTag("RedEnemy"))
        {
            Destroy(gameObject);
            _spawnManager.UpdateScore(5);
        }
        
        if (collision.gameObject.CompareTag("RedSpell") || collision.gameObject.CompareTag("GreenSpell") || collision.gameObject.CompareTag("BlueSpell"))
        {
            Destroy(collision.gameObject);
        }
    }
}
