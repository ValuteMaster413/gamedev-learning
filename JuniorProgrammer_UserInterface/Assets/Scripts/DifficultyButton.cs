using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DifficultyButton : MonoBehaviour
{
    private Button _button;
    private GameManager _gameManager;
    
    public int difficulty = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _button = GetComponent<Button>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _button.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDifficulty()
    {
        _gameManager.StartGame(difficulty);
    }
}
