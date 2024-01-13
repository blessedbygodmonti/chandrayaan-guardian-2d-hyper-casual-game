using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioSource PlayerDeathSound;
    private AudioSource AlienDeathSound;
    //public int maxHealth = 100;
    //public int currentHealth;
   // public HealthBar healthBar;
    public Bullet bulletPrefab;
    public float thrustSpeed = 1.0f;
    public float turnspeed = 1.0f;
    private Rigidbody2D _rigidbody;
    private bool _thrusting;
    private float _turnDirection;
    [SerializeField] private AudioSource ShootingSound;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        _thrusting = Input.GetKey(KeyCode.W);

        if (Input.GetKey(KeyCode.A)) { _turnDirection = 1.0f; }
        else if (Input.GetKey(KeyCode.D)) { _turnDirection = -1.0f; }
        else { _turnDirection = 0.0f; }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
      //  if (Input.GetKeyDown(KeyCode.C))
      // {
       //     TakeDamage(20);
       // }
    }

    private void FixedUpdate()
    {
        if (_thrusting)
        {
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }

        if (_turnDirection != 0.0f)
        {
            _rigidbody.AddTorque(_turnDirection * this.turnspeed);
        }
    }
        
    private void Shoot()
    {
        ShootingSound.Play();
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Alien")
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;
           // _ = this.gameObject.tag == "Alien";
             this.gameObject.SetActive(false);
            //TakeDamage(20);
            //AlienDeathSound.Play();
            Destroy(collision.gameObject);

            FindObjectOfType<GameManager>().PlayerDied();
        }
        
    }

    
    
        
    

    // public void Start()
    //{
    //    currentHealth = maxHealth;
    //     healthBar.SetMaxHealth(maxHealth);
    // }




    // public void TakeDamage(int damage)
    //{
    //    currentHealth -= damage;

    //    healthBar.SetHealth(currentHealth);
    //  }
}

