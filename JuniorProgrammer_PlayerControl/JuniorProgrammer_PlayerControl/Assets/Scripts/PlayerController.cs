using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float turnSpeed = 5.0f;
    private float _horizontalInput;
    private float _verticalInput;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        
        transform.Translate(Vector3.forward * (speed * _verticalInput * Time.deltaTime));
        transform.Rotate(Vector3.up * (turnSpeed * _verticalInput * _horizontalInput * Time.deltaTime));
    }
}
