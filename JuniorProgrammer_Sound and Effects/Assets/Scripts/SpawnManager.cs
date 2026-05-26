using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstacles;
    public Vector3 spawnPosition;
    public int startDelay = 3;
    public int spawnMinInterval = 1;
    public int spawnMaxInterval = 5;

    private PlayerController _playerControllerScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), startDelay, Random.Range(spawnMinInterval, spawnMaxInterval));
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        var obstacleIndex = Random.Range(0, obstacles.Length);

        if (!_playerControllerScript.gameOver)
        {
            Instantiate(obstacles[obstacleIndex], spawnPosition, obstacles[obstacleIndex].transform.rotation);
        }
    }
}
