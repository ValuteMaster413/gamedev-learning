using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float thrustForce = 1f;
    public GameObject boosterFlame;
    public UIDocument uiDocument;
    public GameObject explosionEffect;
    
    private Rigidbody2D _rb;
    private float _elapsedTime = 0f;
    private float _score = 0f;
    public float _scoreMultiplier = 10f;
    private Label _scoreText;
    private Button _restartButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        
        _restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        _restartButton.style.display = DisplayStyle.None;
        _restartButton.clicked += ReloadScene;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        MovePlayer();
    }

    void UpdateScore()
    {
        _elapsedTime += Time.deltaTime;
        _score = Mathf.FloorToInt(_elapsedTime * _scoreMultiplier);
        _scoreText.text = "Score: " + _score;
    }

    void MovePlayer()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 direction = (mousePos - transform.position).normalized;

            transform.up = direction;
            _rb.AddForce(direction * thrustForce);
            
            if (_rb.linearVelocity.magnitude > maxSpeed)
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed;
            }
        }
        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            boosterFlame.SetActive(true);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            boosterFlame.SetActive(false);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        _restartButton.style.display = DisplayStyle.Flex;
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
