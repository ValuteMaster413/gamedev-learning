using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody _targetRb;
    private GameManager _gameManager;
    
    public ParticleSystem particles;
    
    public float minUpForce = 1;
    public float maxUpForce = 1;
    public float minTorqueForce = 1;
    public float maxTorqueForce = 1;
    public float spawnRangeX = 1;
    public float spawnRangeY = 1;
    public int pointValue = 1;
    public bool isDangerous = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _targetRb = GetComponent<Rigidbody>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        _targetRb.AddForce(RandomUpForce(), ForceMode.Impulse);
        _targetRb.AddTorque(RandomTorqueForce(), RandomTorqueForce(), RandomTorqueForce(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnMouseDown()
    {
        if (_gameManager.isGameActive)
        {
            _gameManager.UpdateScore(pointValue);
            if (isDangerous)
            {
                _gameManager.UpdateLives(1);
            }

            Destroy(gameObject);
            Instantiate(particles, transform.position, particles.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!isDangerous && pointValue >= 0)
        {
            _gameManager.UpdateLives(1);
        }
    }

    Vector3 RandomUpForce()
    {
        return Vector3.up * Random.Range(minUpForce, maxUpForce);
    }

    float RandomTorqueForce()
    {
        return Random.Range(minTorqueForce, maxTorqueForce);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnRangeY);
    }
}
