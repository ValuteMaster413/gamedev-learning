using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;
    
    public float speed = 1;
    public float xPos = 15;
    public float zPos = 10;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput =  Input.GetAxis("Horizontal");
        _verticalInput =  Input.GetAxis("Vertical");
        
        Vector3 move = new Vector3(_horizontalInput, 0, _verticalInput);
        move = move.normalized;

        transform.Translate(move * (speed * Time.deltaTime));
        
        Vector3 pos = transform.position;
        
        pos.x = Mathf.Clamp(pos.x, -xPos, xPos);
        pos.z = Mathf.Clamp(pos.z, -zPos, zPos);
        
        transform.position = pos;
    }
}
