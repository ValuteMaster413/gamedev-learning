using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int _score = 0;
    private int _lives = 5;
    public bool isGameActive = true;
    
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    
    public List<GameObject> targets;
    public float spawnRate = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_lives <= 0 || _score < 0)
        {
            GameOver();
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        scoreText.text = "Score: " + _score;
    }

    public void UpdateLives(int livesToSubtract)
    {
        _lives -= livesToSubtract;
        livesText.text = "Lives: " + _lives;
    }

    public void StartGame(int difficulty)
    {
        spawnRate /=  difficulty;
        titleScreen.SetActive(false);
        StartCoroutine(SpawnTargets());
        scoreText.text = "Score: " + _score;
        livesText.text = "Lives: " + _lives;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    IEnumerator SpawnTargets()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
}
