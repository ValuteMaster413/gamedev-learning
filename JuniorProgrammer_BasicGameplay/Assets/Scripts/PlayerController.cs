using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    
    public float speed = 10.0f;
    public float xRange = 15.0f;
    
    private float _horizontalInput;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2) , projectilePrefab.transform.rotation);
        }
        
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        _horizontalInput =  Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * (_horizontalInput * speed * Time.deltaTime));
    }
    
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
