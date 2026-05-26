using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    
    public GameObject[] powerUpPrefabs;
    
    public float spawnRangeX = 1;
    public float spawnRangeZ = 1;
    
    private Vector3 _spawnPos;
    private float _spawnPosX;
    private float _spawnPosZ;
    private int _vaweNumber = 0;
    private int _enemyCount = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    
    private Vector3 GenerateSpawnPos()
    {
        _spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        _spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);
        _spawnPos = new Vector3(-_spawnPosX, 0.5f, -_spawnPosZ);
        
        return _spawnPos;
    }
    
    void SpawnEnemyWave(int enemyToSpawn)
    {
        for (int i = 0; i < enemyToSpawn; i++)
        {
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], GenerateSpawnPos(), enemyPrefabs[Random.Range(0, enemyPrefabs.Length)].transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _enemyCount = FindObjectsByType<EnemyBehavior>(FindObjectsSortMode.None).Length;
        
        if (_enemyCount == 0)
        {
            _vaweNumber++;
            SpawnEnemyWave(_vaweNumber);
            
            if (_vaweNumber % 2 != 0)
            {
                Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], GenerateSpawnPos(), powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)].transform.rotation);
                Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], GenerateSpawnPos(), powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)].transform.rotation);
            }
        }
    }
    
    
}
