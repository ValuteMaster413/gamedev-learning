using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject propeller; 
    
    public float speed = 5.0f;
    public float rotationSpeed;
    private float _horizontalInput;
    private float _verticalInput;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get the user's vertical input
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");

        // move the plane forward at a constant rate
        transform.Translate(Vector3.forward * (speed * _horizontalInput * Time.deltaTime));
        
        propeller.transform.Rotate(Vector3.forward * (speed * 20 * _horizontalInput * Time.deltaTime));

        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(Vector3.right * (rotationSpeed * _verticalInput * Time.deltaTime));
    }
}
