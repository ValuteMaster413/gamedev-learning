using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private const int SpawnRangeX = 20;
    private const int SpawnPosZ = 29;
    public int startDelay = 3;
    public int spawnInterval = 2;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnRandomAnimal), startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SpawnRandomAnimal()
    {
        var animalIndex = Random.Range(0, animalPrefabs.Length);
        var randomXPos = Random.Range(-SpawnRangeX, SpawnRangeX + 1);
            
        Instantiate(animalPrefabs[animalIndex], new Vector3(randomXPos, 0, SpawnPosZ), animalPrefabs[animalIndex].transform.rotation);
    }
}
