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
    public bool isBoss = false;
    public bool runAway = false;
    private float speed;
    public GameObject blood;


    private AudioSource audioSource;
    public AudioClip growl1;
    public AudioClip growl2;
    public AudioClip growl3;
    public AudioClip moan1;
    public AudioClip moan2;
    public AudioClip death;
    

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
    public GameObject BossHealthBar;
    private GameObject _waveManager;
    public int _wave;
    public bool isDead = false;

    // distance for audio volume
    private float distanceToPlayer;

    private void Start()
    {
        //To save the original Material Type
        _normalMaterial = gameObject.GetComponent<Renderer>().material;

        //get wave number
        _waveManager = GameObject.FindWithTag("WaveManager");
        _wave = _waveManager.GetComponent<WaveManager>().wave;

        //Debug.Log("speed is " + _agent.speed);

        //To get the NavMeshAgent Component
        _agent = GetComponent<NavMeshAgent>();
        speed = Random.Range(5f, 10f + 2*_wave);
        _agent.speed = speed;
        maxHealth = health;
       

        //For Boss Health Bar to be a Global Component
        if (isBoss)
        {
            //Set Boss UI to be True
            BossHealthBar.SetActive(true);

            healthBar = BossHealthBar.GetComponentInChildren<HealthBar>();
        }
        else
        {
            healthBar = GetComponentInChildren<HealthBar>();
        }

        healthBar.UpdateHealthBar(health, maxHealth);

        player = GameObject.FindWithTag("Player");
        _spawner = GameObject.FindWithTag("Spawner");

        // calculate distance to play sound at volume 
        distanceToPlayer = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(player.transform.position.x - _agent.transform.position.x), 2) + Mathf.Pow(Mathf.Abs(player.transform.position.z - _agent.transform.position.z), 2));
        Debug.Log("distance: " + distanceToPlayer);
        // normalize distance using max audio distance
        distanceToPlayer = distanceToPlayer / 300;
       

        audioSource = GetComponent<AudioSource>();
        if (UnityEngine.Random.Range(0, 3) == 0)
        
        {
            switch (UnityEngine.Random.Range(0, 3))
            {
                case 0:
                    audioSource.PlayOneShot(growl1, 1);
                    if (distanceToPlayer < 1)
                    {
                        audioSource.volume = 1 - distanceToPlayer;
                    }
                    else
                    {
                        audioSource.volume = 0;
                    }
                    break;
                case 1:
                    audioSource.PlayOneShot(growl2, 1);
                    if (distanceToPlayer < 1)
                    {
                        audioSource.volume = 1 - distanceToPlayer;
                    }
                    else
                    {
                        audioSource.volume = 0;
                    }
                    break;
                case 2:
                    audioSource.PlayOneShot(growl3, 1);
                    if (distanceToPlayer < 1)
                    {
                        audioSource.volume = 1 - distanceToPlayer;
                    }
                    else
                    {
                        audioSource.volume = 0;
                    }
                    break;
            }
        }
    }

    private void Update()
    {
        if (runAway)
        {
            _agent.destination = transform.position - player.transform.position;
        }
        else
        {
            //To start the movement of the Agent towards the player
            _agent.destination = player.transform.position;
        }

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

        // calculate distance to play sound at volume 
        distanceToPlayer = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(player.transform.position.x - _agent.transform.position.x), 2) + Mathf.Pow(Mathf.Abs(player.transform.position.z - _agent.transform.position.z), 2));
        
        // normalize distance using max audio distance
        distanceToPlayer = distanceToPlayer / 300;
        

        if (_time >= 1.5)
        {
            if (UnityEngine.Random.Range(0, 10) == 0)
            {
                switch (UnityEngine.Random.Range(0, 5))
                {
                    case 0:
                        audioSource.PlayOneShot(growl1, 1);
                        if (distanceToPlayer < 1) {
                            audioSource.volume = 1 - distanceToPlayer;
                        }
                        else
                        {
                            audioSource.volume = 0;
                        }
                        
                        break;
                    case 1:
                        audioSource.PlayOneShot(growl2, 1);
                        if (distanceToPlayer < 1)
                        {
                            audioSource.volume = 1 - distanceToPlayer;
                        }
                        else
                        {
                            audioSource.volume = 0;
                        }
                        break;
                    case 2:
                        audioSource.PlayOneShot(growl3, 1);
                        if (distanceToPlayer < 1)
                        {
                            audioSource.volume = 1 - distanceToPlayer;
                        }
                        else
                        {
                            audioSource.volume = 0;
                        }
                        break;
                    case 3:
                        audioSource.PlayOneShot(moan1, 1);
                        if (distanceToPlayer < 1)
                        {
                            audioSource.volume = 1 - distanceToPlayer;
                        }
                        else
                        {
                            audioSource.volume = 0;
                        }
                        break;
                    case 4:
                        audioSource.PlayOneShot(moan2, 1);
                        if (distanceToPlayer < 1)
                        {
                            audioSource.volume = 1 - distanceToPlayer;
                        }
                        else
                        {
                            audioSource.volume = 0;
                        }
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
                GameObject bloodParticle = Instantiate(blood, gameObject.transform.position, Quaternion.identity);
                //bloodParticle.transform.SetParent(gameObject.transform);
                ParticleSystem bloodPS = bloodParticle.GetComponent<ParticleSystem>();
                if (bloodPS != null)
                {
                    bloodPS.Play();
                }

                
                health--;
                _damageBool = false;
                _damageTimer = 0;
                gameObject.GetComponent<Renderer>().material = damagedMaterial;
                healthBar.UpdateHealthBar(health, maxHealth);
            }
            //Destroy(collision.gameObject);

            if (health <= 0 && !isDead)
            {
                isDead = true;
                audioSource.PlayOneShot(death);
                gameObject.GetComponent<Collider>().enabled = false;

                if (isBoss)
                {
                    BossHealthBar.SetActive(false);
                }
                _spawner.GetComponent<ZombieSpawner>().zombieCounter(-1);
                                
                Destroy(this.gameObject);
                              

            }
        }


        //IF Collision is from the PLAYER
        if (collision.gameObject.tag == "Player")
        {
            //Access the Player's Health here some how
            //collision.gameObject.health--;
        }
    }

    public float GetDistanceFromPlayer()
    {
        return Mathf.Abs(Vector3.Distance(player.transform.position, transform.position));
    }

}
