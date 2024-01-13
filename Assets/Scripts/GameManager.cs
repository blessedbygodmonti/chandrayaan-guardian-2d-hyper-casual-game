using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    public GameObject InsideTimer;

    public GameObject cooldownTimer;

    public GameObject overheated;

    public GameObject RTF;

    public GameObject Chandrayaann;

    //public Behaviour Unwanted;

    //public HealthBar healthBar;

    //public int maxHealth = 100;

    //public int currentHealth;

    //Spublic Alien alien;

    public AlienSpawner alienSpawner;

    public Player player;

    public ParticleSystem explosion;

    public float respawnTime = 3.0f;

    public float respawnInvulnerabilityTime = 3.0f;

    public int lives = 3;
   
    public int score = 0;

    

    [SerializeField] private AudioSource AlienDeathSound;

    [SerializeField] private AudioSource BGM;

    [SerializeField] private AudioSource PlayerDeathSound;

    [SerializeField] private AudioSource GameOverSound;

    [SerializeField] private AudioSource DestroyChandrayaanSound;

    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private TextMeshProUGUI scoreText;
  
    [SerializeField] private TextMeshProUGUI livesText;

   
   // public static GameManager Instance { get; private set; }

   // public int score { get; private set; }
   // public int lives { get; private set; }

  

        private void Start()
    {
        gameOverUI.SetActive(false);
        //currentHealth = maxHealth;
       // healthBar.SetMaxHealth(maxHealth);
    }
    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }

       // if (Input.GetKeyDown(KeyCode.H))
      //  {
      //      this.player.gameObject.SetActive(false);

      //  }
  //      if(Input.GetKeyDown(KeyCode.J))
  //      {
  //          PlayerDeathSound.Play();
  //          Invoke(nameof(Respawn), this.respawnTime);
  //      }
    }


    private void NewGame()
    {
       SceneManager.LoadSceneAsync(1);
       // Alien[] aliens = FindObjectsOfType<Alien>();

       // for (int i = 0; i < aliens.Length; i++)
      //  {
      //      Destroy(aliens[i].gameObject);
      //  }

       // gameOverUI.SetActive(false);

      //  SetScore(0);
      //  SetLives(3);
      //  Respawn();
    }

    public void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    public void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }



    public void AlienDestroyed(Alien alien)
    {
       
        this.explosion.transform.position = alien.transform.position;
        this.explosion.Play();
        AlienDeathSound.Play();
        

        if (alien.size < 0.7f)
        {
            SetScore(score + 100);
        }
        else if (alien.size < 1.4f)
        {
            SetScore(score + 50);
        }
        else
        {
            SetScore(score + 25);
        }
    }




    public void PlayerDied()
    {
        PlayerDeathSound.Play();
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        //this.lives--;
        SetLives(lives - 1);

        if (this.lives <= 0)
        {
            //object value = "Chandrayaan".SetActive(false);
           GameOver();
            Chandrayaann.gameObject.SetActive(false);

        }
        else
       {
           Invoke(nameof(Respawn), this.respawnTime);
        }

      //  if (this.currentHealth <= 0)
      //  {
      //      GameOver();
       // }
       // else
       // {
        //    Invoke(nameof(Respawn), this.respawnTime);
       // }
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
    public void GameOver()
    {
        cooldownTimer.SetActive(false);
        InsideTimer.SetActive(false);
        overheated.SetActive(false);
        RTF.SetActive(false);
        //Unwanted.enabled = false;
        //Destroy(alien);
        Destroy(alienSpawner);
        gameOverUI.SetActive(true);
        DestroyChandrayaanSound.Play();
        GameOverSound.Play();
        this.lives = 0;
        this.score = 0;
        this.player.gameObject.SetActive(false);
        // Invoke(nameof(Respawn), this.respawnTime);
    }

   // public void TakeDamage(int damage)
   // {
    //    currentHealth -= damage;

    //    healthBar.SetHealth(currentHealth);
   // }



}
