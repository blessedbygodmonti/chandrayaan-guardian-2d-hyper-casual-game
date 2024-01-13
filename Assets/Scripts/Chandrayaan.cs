using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chandrayaan : MonoBehaviour

{
    public GameObject RTF; 
    public GameObject overheated;
    public GameObject InsideTimer;
    public GameObject cooldownTimer;
    public float cooldownTime = 0;
    public float InsideTime = 0;
    public bool JPressed;
    public bool HPressed;
    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityTime = 3.0f;
    public Player player;
    public bool Player ;
    //public float bulletspawncount;
    private bool _thrusting;
    public float thrustSpeed = 1.0f;
    public float turnspeed = 1.0f;
    private float _turnDirection;
    private Rigidbody2D _rigidbody;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public ParticleSystem explosion;
    public Bullet bulletPrefab;
    [SerializeField] private AudioSource ShootingSound;
    [SerializeField] private AudioSource ChandAlienCollision;
    [SerializeField] private AudioSource PlayerDeathSound;
    [SerializeField] TextMeshProUGUI cooldownText;
    [SerializeField] TextMeshProUGUI InsideTimeText;
    [SerializeField] TextMeshProUGUI Overheated;
    [SerializeField] TextMeshProUGUI ReadytoFire;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

 
    private void Shoot()
    {
        ShootingSound.Play();
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Alien")
        {
            ChandAlienCollision.Play();
            TakeDamage(20);
           Destroy(collision.gameObject);

            if(currentHealth <= 0)
            {
                this.explosion.Play();
                Destroy(gameObject);
                FindObjectOfType<GameManager>().GameOver();
            }
             
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    public void Start()
    {
        RTF.SetActive(false);
        overheated.SetActive(false);
        //cooldownTime = InsideTime;
        InsideTimer.gameObject.SetActive(false);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {////Coroutine

        cooldownText.text = cooldownTime.ToString("0");
        InsideTimeText.text = InsideTime.ToString("0");


      //print(cooldownTime);
       // print(InsideTime);
       cooldownTime -= 1 * Time.deltaTime;
        InsideTime -= 1 * Time.deltaTime;
        

       if(cooldownTime <= 0) 
        {
            cooldownTimer.gameObject.SetActive(false);
            cooldownTime = 0;
          //  cooldownText.text = cooldownTime.ToString("Ready to Fire");
            Overheated.gameObject.SetActive(false);
            RTF.gameObject.SetActive(true);
            


            //  }

            //  if(InsideTime <= 0) 
            //  {
            //     // InsideTimer.gameObject.SetActive(false);
            //     InsideTime = 0;
            //    // InsideTimeText.text = InsideTime.ToString("Overheated");

        }

        if (Player == true) 
        {
            _rigidbody.angularVelocity = 0;
            transform.rotation = Quaternion.identity;
        }

        if (Input.GetKeyDown(KeyCode.H) && cooldownTime <= 0)
        {
            HPressed = true;

            if (HPressed == true && Player == true)
            {
               // RTF.gameObject.SetActive(false);
                cooldownTimer.gameObject.SetActive(false);
                //InsideTime += 1 * Time.deltaTime;
                InsideTimer.gameObject.SetActive(true);
                InsideTime = 10;
                this.player.gameObject.SetActive(false);
                Player = false;
            }
        }
      
      //    if (Input.GetKeyDown(KeyCode.J) || InsideTime <= 0)
      //     {
      //     // Overheated.gameObject.SetActive(true);
      //      InsideTimer.gameObject.SetActive(false);
      //      cooldownTimer.gameObject.SetActive(true);
           

      //      JPressed = true;
      //      if(JPressed == true && Player == false) 
      //     {
      //          //this.player.gameObject.SetActive(true);
      //          cooldownTime = 5;
      //          Player = true;
      //       PlayerDeathSound.Play();
      //      Invoke(nameof(Respawn), this.respawnTime);


      //    }
      //  }

        if (Input.GetKeyDown(KeyCode.J) )
        {
            
            JPressed = true;
            if (JPressed == true && Player == false)
            {
                RTF.gameObject.SetActive(false);
                InsideTimer.gameObject.SetActive(false);
                cooldownTimer.gameObject.SetActive(true);
                Overheated.gameObject.SetActive(true);
                //this.player.gameObject.SetActive(true);
                cooldownTime = 5;
                Player = true;
                PlayerDeathSound.Play();
                Invoke(nameof(Respawn), this.respawnTime);


            }
        }

        if ( InsideTime <= 0)
        {
            
            //  InsideTimer.gameObject.SetActive(false);



            JPressed = true;
            if (JPressed == true && Player == false)
            {
                InsideTimer.gameObject.SetActive(false);
                RTF.gameObject.SetActive(false);
                Overheated.gameObject.SetActive(true);
                cooldownTimer.gameObject.SetActive(true);
                //this.player.gameObject.SetActive(true);
                cooldownTime = 5;
                Player = true;
                PlayerDeathSound.Play();
                Invoke(nameof(Respawn), this.respawnTime);


            }
        }

        if (Input.GetKeyUp(KeyCode.H))
        {
            HPressed = false;
        }


        if (Input.GetKeyUp(KeyCode.J))
        {
            JPressed= false;
        }


        if (Player==false)
        {
            _thrusting = Input.GetKey(KeyCode.W);

            if (Input.GetKey(KeyCode.A)) { _turnDirection = 1.0f; }
            else if (Input.GetKey(KeyCode.D)) { _turnDirection = -1.0f; }
            else { _turnDirection = 0.0f; }

            if //(Input.GetKeyDown(KeyCode.Space) ||
                ( Input.GetMouseButtonDown(0))
            {
                Shoot();

            }
        }
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
    public void Respawn()
    {

        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.gameObject.SetActive(true);

        Invoke(nameof(TurnOnCollisions), this.respawnInvulnerabilityTime);
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}