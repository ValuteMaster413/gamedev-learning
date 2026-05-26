using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private float _horizontalInput;
    public float rotationSpeed = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {/*
        _horizontalInput =  Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, _horizontalInput * rotationSpeed * Time.deltaTime);
        
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -1 * rotationSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, 1 * rotationSpeed * Time.deltaTime);
        }
        */
    }
}
