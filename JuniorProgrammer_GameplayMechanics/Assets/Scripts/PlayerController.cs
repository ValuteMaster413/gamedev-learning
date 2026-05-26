using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private GameObject _focalPoint;
    
    public GameObject powerupIndicator;
    
    private float _forwardInput;
    private float _sidewaysInput;
    private bool _hasPowerup = false;
    private float _basicStrength = 3.0f;
    private float _powerupStrength = 5.0f;
    
    public float speed = 1;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody =  GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        _forwardInput = Input.GetAxis("Vertical");
        _sidewaysInput = Input.GetAxis("Horizontal");
        _rigidbody.AddForce(Vector3.forward * (_forwardInput * speed));
        _rigidbody.AddForce(Vector3.right * (_sidewaysInput * speed));
        
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.3f, 0); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            _hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.SetActive(true);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        _hasPowerup = false;
         powerupIndicator.SetActive(false);
    }

    void OnCollisionEnter(Collision other)
    {
        Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position; 
        
        if (other.gameObject.CompareTag("Enemy") && _hasPowerup)
        {
            enemyRigidbody.AddForce(awayFromPlayer * _basicStrength * _powerupStrength, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("Enemy") && !_hasPowerup)
        {
            enemyRigidbody.AddForce(awayFromPlayer * _basicStrength, ForceMode.Impulse);
        }
    }
}
