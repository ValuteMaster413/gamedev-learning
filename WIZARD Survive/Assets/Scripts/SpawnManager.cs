using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    
    public GameObject[] spellPrefabs;
    
    public GameObject[] boostPrefabs;

    public AudioClip enemyAppearanceSound;
    
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public GameObject infoScreen;
    public Button startButton;
    public Button restartButton;
    public GameObject _player;
        
    public float spawnRangeX = 1;
    public float spawnRangeZ = 1;
    
    private PlayerController _playerController;
    private AudioSource _audioSource;
    private Vector3 _spawnPos;
    private float _spawnPosX;
    private float _spawnPosZ;
    private int _waveNumber = 0;
    private int _enemyCount = 0;
    private int _score = 0;
    public bool isGameActive = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        _audioSource = GetComponent<AudioSource>();
    }
    
    private Vector3 GenerateSpawnPos()
    {
        _spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        _spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);
        _spawnPos = new Vector3(-_spawnPosX, 0.5f, -_spawnPosZ);
        
        return _spawnPos;
    }
    
    void StartGame()
    {
        titleScreen.SetActive(false);
        infoScreen.SetActive(true);
        Instantiate(_player, new Vector3(0, 0.5f, 0), _player.transform.rotation);
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        isGameActive = true;
        StartCoroutine(SpawnTargets());
        
    } 
    
    public void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        infoScreen.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = "Score: " + _score;
    }

    public void UpdateLives()
    {
        infoScreen.transform.Find("LiveCounter").GetComponent<TextMeshProUGUI>().text = "Lives: " + _playerController.life;
    }

    public void UpdateSpells(int spellToUpdate)
    {
        if (spellToUpdate == 0)
        {
            infoScreen.transform.Find("BlueSpellCounter").GetComponent<TextMeshProUGUI>().text = "Blue Spells: " + _playerController.blueSpellCount;
        }
        else if (spellToUpdate == 1)
        {
            infoScreen.transform.Find("GreenSpellCounter").GetComponent<TextMeshProUGUI>().text = "Green Spells: " + _playerController.greenSpellCount;
        }
        else if (spellToUpdate == 2)
        {
            infoScreen.transform.Find("RedSpellCounter").GetComponent<TextMeshProUGUI>().text = "Red Spells:" + _playerController.redSpellCount;
        }
        else if (spellToUpdate == 3)
        {
            infoScreen.transform.Find("UberSpellCounter").GetComponent<TextMeshProUGUI>().text = "Uber Spells: " + _playerController.uberSpellCount;
        }
    }
    
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    void SpawnEnemyWave(int enemyToSpawn)
    {
        for (int i = 0; i < enemyToSpawn; i++)
        {
            
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], GenerateSpawnPos(), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    IEnumerator SpawnTargets()
    {
        while (isGameActive)
        {
            _enemyCount = FindObjectsByType<EnemyBehavior>(FindObjectsSortMode.None).Length;

            yield return new WaitForSeconds(1f);
            
            if (_enemyCount == 0)
            {
                yield return new WaitForSeconds(1f);

                _audioSource.PlayOneShot(enemyAppearanceSound, 1.0f);

                _waveNumber++;
                SpawnEnemyWave(_waveNumber);
                
                if (_waveNumber % 2 != 0)
                {
                    GameObject spell = Instantiate(spellPrefabs[Random.Range(0, spellPrefabs.Length)], GenerateSpawnPos(), Quaternion.identity); 
                    spell.transform.Find("Gem").gameObject.SetActive(true); 
                    
                    spell = Instantiate(spellPrefabs[Random.Range(0, spellPrefabs.Length)], GenerateSpawnPos(), Quaternion.identity); 
                    spell.transform.Find("Gem").gameObject.SetActive(true);
                }

                if (_waveNumber % 3 != 0)
                {
                    Instantiate(boostPrefabs[Random.Range(0, boostPrefabs.Length)], GenerateSpawnPos(), Quaternion.identity);
                }
            }
        }
        
        gameOverScreen.SetActive(true);
        infoScreen.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        StopAllCoroutines();
    }
}
