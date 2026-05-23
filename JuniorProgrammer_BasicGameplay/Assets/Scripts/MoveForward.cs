using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 10.0f;

    public float topBound = 30;
    public float bottomBound = -10;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));

        if (transform.position.z > topBound || transform.position.z < bottomBound)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
