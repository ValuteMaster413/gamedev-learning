using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    
    public float spawnRange = 1;
    private float _spawnPosX;
    private float _spawnPosZ;
    private Vector3 _spawnPos;
    private int _enemyCount = 0;
    private int _vaweNumber = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
                Instantiate(powerUpPrefab, GenerateSpawnPos(), powerUpPrefab.transform.rotation);
            }
        }
    }

    void SpawnEnemyWave(int enemyToSpawn)
    {
        for (int i = 0; i < enemyToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPos()
    {
        _spawnPosX = Random.Range(-spawnRange, spawnRange);
        _spawnPosZ = Random.Range(-spawnRange, spawnRange);
        _spawnPos = new Vector3(-_spawnPosX, 0, -_spawnPosZ);
        
        return _spawnPos;
    }
}
