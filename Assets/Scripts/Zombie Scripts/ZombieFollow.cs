using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


//This code will ONLY work with a NavMeshAgent attached to the Enemy.
//This code will ONLY work with a NavMesh Surfave in Place.


public class ZombieFollow : MonoBehaviour
{

    private GameObject player;
    public float health = 3;
    private float maxHealth;
    public float timeBetweenHits = 0.5f;
    public bool noDamageCooldown = true;
    


    private AudioSource audioSource;
    public AudioClip growl1;
    public AudioClip growl2;
    public AudioClip growl3;
    public AudioClip moan1;
    public AudioClip moan2;
    

    public Material damagedMaterial;

    private Material _normalMaterial;

    //To keep track of the time since the enemy was last damaged
    private float _damageTimer = 0;
    private float _time = 0;
    //This is the variable determining if the enemy CAN take damage again
    private bool _damageBool = true;

    private NavMeshAgent _agent;

    //This is the enemies health bar
    private HealthBar healthBar;

    //Zombie counter script
    private GameObject _spawner;

    //coint system
    public Coins coins;

    private void Start()
    {
        //To save the original Material Type
        _normalMaterial = gameObject.GetComponent<Renderer>().material;

        //To get the NavMeshAgent Component
        _agent = GetComponent<NavMeshAgent>();
        maxHealth = health;
        healthBar = GetComponentInChildren<HealthBar>();
        player = GameObject.FindWithTag("Player");
        _spawner = GameObject.FindWithTag("Spawner");

        audioSource = GetComponent<AudioSource>();
        if (UnityEngine.Random.Range(0, 3) == 0)
        {
            switch (UnityEngine.Random.Range(0, 3))
            {
                case 0:
                    audioSource.PlayOneShot(growl1, 1);
                    break;
                case 1:
                    audioSource.PlayOneShot(growl2, 1);
                    break;
                case 2:
                    audioSource.PlayOneShot(growl3, 1);
                    break;
            }
        }
    }

    private void Awake()
    {
       
        
    }

    private void Update()
    {
        //To start the movement of the Agent towards the player
        _agent.destination = player.transform.position;

        //Damage Timer, for Time Between Hits
        if (_damageTimer > timeBetweenHits)
        {
            gameObject.GetComponent<Renderer>().material = _normalMaterial;
            _damageBool = true;
        }
        else
        {
            _damageTimer += Time.deltaTime;
        }

        _time += Time.deltaTime;

        if (_time >= 1.5)
        {
            if (UnityEngine.Random.Range(0, 10) == 0)
            {
                switch (UnityEngine.Random.Range(0, 5))
                {
                    case 0:
                        audioSource.PlayOneShot(growl1, 1);
                        break;
                    case 1:
                        audioSource.PlayOneShot(growl2, 1);
                        break;
                    case 2:
                        audioSource.PlayOneShot(growl3, 1);
                        break;
                    case 3:
                        audioSource.PlayOneShot(moan1, 1);
                        break;
                    case 4:
                        audioSource.PlayOneShot(moan2, 1);
                        break;
                }
            }

            _time = 0;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //IF collision is from a BULLET
        if (collision.gameObject.tag == "Bullet")
        {
            if (_damageBool || noDamageCooldown)
            {
                health--;
                _damageBool = false;
                _damageTimer = 0;
                gameObject.GetComponent<Renderer>().material = damagedMaterial;
                healthBar.UpdateHealthBar(health, maxHealth);
            }
            Destroy(collision.gameObject);

            if (health <= 0)
            {
                Destroy(this.gameObject);
                _spawner.GetComponent<ZombieSpawner>().zombieCounter(-1);
                //coins.coinCounter(Random.Range(3, 8));
            }
        }



        //IF Collision is from the PLAYER
        if (collision.gameObject.tag == "Player")
        {
            //Access the Player's Health here some how
            //collision.gameObject.health--;
        }
    }



    public void Slow()
    {

    }

}
