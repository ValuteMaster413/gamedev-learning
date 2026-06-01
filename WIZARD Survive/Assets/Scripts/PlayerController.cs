using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private Animator _animator;
    
    public GameObject[] spellPrefab;
    private float _horizontalInput;
    private float _verticalInput;
    private int _selectedSpell = 0;
    private int _spellSpeed = 50;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;
    
    public GameObject infoScreen;
    public GameObject spellCountScreen;
    public AudioClip castSound;
    public AudioClip damageSound;
    public AudioClip deadSound;
    public AudioClip pickupSound;
    public int blueSpellCount = 0;
    public int redSpellCount = 0;
    public int greenSpellCount = 0;
    public int uberSpellCount = 10;
    public float speed = 1;
    public int life = 5;
    public float xPos = 15;
    public float zPos = 10;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        infoScreen = GameObject.Find("InfoScreen");
        spellCountScreen  = GameObject.Find("SpellCountScreen");
        _audioSource = GetComponent<AudioSource>();
        
        spellCountScreen.transform.Find("BlueSpellCounter").GetComponent<TMP_Text>().fontMaterial.SetFloat("_OutlineWidth", 0f);
        spellCountScreen.transform.Find("GreenSpellCounter").GetComponent<TMP_Text>().fontMaterial.SetFloat("_OutlineWidth", 0f);
        spellCountScreen.transform.Find("RedSpellCounter").GetComponent<TMP_Text>().fontMaterial.SetFloat("_OutlineWidth", 0f);
        spellCountScreen.transform.Find("UberSpellCounter").GetComponent<TMP_Text>().fontMaterial.SetFloat("_OutlineWidth", 0f);
        spellCountScreen.transform.Find("BlueSpellCounter").GetComponent<TMP_Text>().fontMaterial.SetFloat("_OutlineWidth", 0.3f);

    }

    void Update()
    {
        if (_spawnManager.isGameActive && life > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (_selectedSpell == 0 && blueSpellCount > 0)
                {
                    ShootSpell();
                    blueSpellCount -= 1;
                }

                if (_selectedSpell == 1 && greenSpellCount > 0)
                {
                    ShootSpell();
                    greenSpellCount -= 1;
                }

                if (_selectedSpell == 2 && redSpellCount > 0)
                {
                    ShootSpell();
                    redSpellCount -= 1;
                }

                if (_selectedSpell == 3 && uberSpellCount > 0)
                {
                    ShootSpell();
                    uberSpellCount -= 1;
                }
                
                _audioSource.PlayOneShot(castSound, 1.0f);
                _spawnManager.UpdateSpells(_selectedSpell);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _selectedSpell = 0;
                UpdateSelectedUI(_selectedSpell);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _selectedSpell = 1;
                UpdateSelectedUI(_selectedSpell);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _selectedSpell = 2;
                UpdateSelectedUI(_selectedSpell);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                _selectedSpell = 3;
                UpdateSelectedUI(_selectedSpell);
            }
        }
        else
        {
            _animator.SetBool("Death_b", true);
            _spawnManager.isGameActive = false;
        }
    }
    
    void ShootSpell()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        var plane = new Plane(Vector3.up, transform.position);

        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 mouseWorldPos = ray.GetPoint(distance);
            
            Vector3 direction = (mouseWorldPos - transform.position).normalized;
            
            Vector3 spawnPos = transform.position + direction * 2f;
            
            GameObject projectile = Instantiate(
                spellPrefab[_selectedSpell],
                spawnPos,
                Quaternion.identity
            );
            
            projectile.transform.Find("Gem").gameObject.SetActive(false);
            projectile.transform.Find("Particle").gameObject.SetActive(true);
            if (_selectedSpell == 3)
            {
                projectile.transform.Find("Particle1").gameObject.SetActive(true);
                projectile.transform.Find("Particle2").gameObject.SetActive(true);
            }
            
            projectile.transform.forward = direction;
            
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezePositionY;

            rb.linearVelocity = direction * _spellSpeed;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_spawnManager.isGameActive)
        {

            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            Vector3 move = new Vector3(x, 0, z);

            if (move.sqrMagnitude > 1)
                move.Normalize();

            Vector3 targetPosition = _rigidBody.position + move * (speed * Time.fixedDeltaTime);

            _rigidBody.MovePosition(targetPosition);

            _rigidBody.linearVelocity = Vector3.zero;
            _rigidBody.angularVelocity = Vector3.zero;

            if (move != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move);
                _rigidBody.rotation = Quaternion.Slerp(
                    _rigidBody.rotation,
                    targetRotation,
                    15f * Time.fixedDeltaTime
                );
            }

            _animator.SetFloat("Speed_f", move.magnitude);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("UberSpell"))
        {
            SpellBehavior spellBehavior = collision.gameObject.GetComponent<SpellBehavior>();
            if (collision != null && !spellBehavior.alreadyConsumed)
            {
                spellBehavior.alreadyConsumed = true;
                uberSpellCount += 5;
                _spawnManager.UpdateSpells(3);
                _audioSource.PlayOneShot(pickupSound, 1.0f);
                Destroy(collision.gameObject);
            }
        }
        
        if (collision.gameObject.CompareTag("BlueSpell"))
        {
            SpellBehavior spellBehavior = collision.gameObject.GetComponent<SpellBehavior>();
            if (collision != null && !spellBehavior.alreadyConsumed)
            {
                spellBehavior.alreadyConsumed = true;
                blueSpellCount += 5;
                _spawnManager.UpdateSpells(0);
                _audioSource.PlayOneShot(pickupSound, 1.0f);
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("RedSpell"))
        {
            SpellBehavior spellBehavior = collision.gameObject.GetComponent<SpellBehavior>();
            if (collision != null && !spellBehavior.alreadyConsumed)
            {
                spellBehavior.alreadyConsumed = true;
                redSpellCount += 5;
                _spawnManager.UpdateSpells(2);
                _audioSource.PlayOneShot(pickupSound, 1.0f);
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("GreenSpell"))
        {
            SpellBehavior spellBehavior = collision.gameObject.GetComponent<SpellBehavior>();
            if (collision != null && !spellBehavior.alreadyConsumed)
            {
                spellBehavior.alreadyConsumed = true;
                greenSpellCount += 5;
                _spawnManager.UpdateSpells(1);
                _audioSource.PlayOneShot(pickupSound, 1.0f);
                Destroy(collision.gameObject);
            }
        }
        
        if (collision.gameObject.CompareTag("VitalityBoost"))
        {
            BoostBehavior boostBehavior = collision.gameObject.GetComponent<BoostBehavior>();
            if (collision != null && !boostBehavior.alreadyConsumed)
            {
                boostBehavior.alreadyConsumed = true;
                life++;
                _spawnManager.UpdateLives();
                _audioSource.PlayOneShot(pickupSound, 1.0f);
                Destroy(collision.gameObject);
            }
        }
        
        if (collision.gameObject.CompareTag("BlueEnemy") || collision.gameObject.CompareTag("GreenEnemy") || collision.gameObject.CompareTag("RedEnemy"))
        {
            EnemyBehavior enemyBehavior = collision.gameObject.GetComponent<EnemyBehavior>();
            
            if (collision != null && !enemyBehavior.alreadyHit)
            {
                enemyBehavior.alreadyHit = true;
                
                life -= 1;
                _spawnManager.UpdateLives();
                if (life == 0)
                {
                    _audioSource.PlayOneShot(deadSound, 1.0f);
                }
                else
                {
                    _audioSource.PlayOneShot(damageSound, 1.0f);
                }

                Destroy(collision.gameObject);
            }
        }
    }
    
    void UpdateSelectedUI(int selected)
    {
        spellCountScreen.transform.Find("BlueSpellCounter").GetComponent<TMP_Text>().fontMaterial.SetFloat("_OutlineWidth", 0f);
        spellCountScreen.transform.Find("GreenSpellCounter").GetComponent<TMP_Text>().fontMaterial.SetFloat("_OutlineWidth", 0f);
        spellCountScreen.transform.Find("RedSpellCounter").GetComponent<TMP_Text>().fontMaterial.SetFloat("_OutlineWidth", 0f);
        spellCountScreen.transform.Find("UberSpellCounter").GetComponent<TMP_Text>().fontMaterial.SetFloat("_OutlineWidth", 0f);
        
        switch (selected)
        {
            case 0:
                spellCountScreen.transform.Find("BlueSpellCounter").GetComponent<TMP_Text>().fontMaterial.SetFloat("_OutlineWidth", 0.3f);
                break;
            case 1:
                spellCountScreen.transform.Find("GreenSpellCounter").GetComponent<TMP_Text>().fontMaterial.SetFloat("_OutlineWidth", 0.3f);
                break;
            case 2:
                spellCountScreen.transform.Find("RedSpellCounter").GetComponent<TMP_Text>().fontMaterial.SetFloat("_OutlineWidth", 0.3f);
                break;
            case 3:
                spellCountScreen.transform.Find("UberSpellCounter").GetComponent<TMP_Text>().fontMaterial.SetFloat("_OutlineWidth", 0.3f);
                break;
        }
    }
}
