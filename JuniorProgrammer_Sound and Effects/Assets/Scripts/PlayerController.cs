using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRb;
    private Animator _playerAnimator;
    private AudioSource _playerAudio;
   
    public ParticleSystem deathEffect;
    public ParticleSystem runEffect;
    
    public AudioClip jumpSound;
    public AudioClip crashSound;
    
    public float jumpForce = 10;
    public float gravityModifier = 1;
    public bool isOnGround = true;
    
    public bool gameOver = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
        
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            _playerRb.AddForce(Vector3.up * jumpForce,  ForceMode.Impulse);
            isOnGround = false;
            
            _playerAnimator.SetTrigger("Jump_trig");
            
            runEffect.Stop();
            
            _playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            
            runEffect.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over");
            
            _playerAnimator.SetBool("Death_b", true);
            _playerAnimator.SetInteger("DeathType_int", 1);
            
            runEffect.Stop();
            deathEffect.Play();
            
            _playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
