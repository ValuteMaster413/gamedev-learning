using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float _speed = 10;
    
    private PlayerController _playerControllerScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * (_speed * Time.deltaTime));
        }
        
        if (transform.position.x < -30  && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
