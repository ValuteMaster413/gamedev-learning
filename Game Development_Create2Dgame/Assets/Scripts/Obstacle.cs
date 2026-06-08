using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject bounceEffectPrefab;
    
    private readonly float _minSize = 0.5f;
    private readonly float _maxSize = 2.0f;
    private readonly float _minSpeed = 100f;
    private readonly float _maxSpeed = 300f;
    private readonly float _maxSpinSpeed = 10f;
    private Rigidbody2D _rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        float randomSize = Random.Range(_minSize, _maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1);
        
        float randomSpeed = Random.Range(_minSpeed, _maxSpeed) / randomSize;
        Vector2 randomDirection = Random.insideUnitCircle;
        _rb.AddForce(randomDirection * randomSpeed);
        
        float randomTorque = Random.Range(-_maxSpinSpeed, _maxSpinSpeed);
        _rb.AddTorque(randomTorque);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactPoint = collision.GetContact(0).point; 
        GameObject bounceEffect = Instantiate(bounceEffectPrefab, contactPoint, Quaternion.identity);

        // Destroy the effect after 1 second
        Destroy(bounceEffect, 1f);
    }
}
