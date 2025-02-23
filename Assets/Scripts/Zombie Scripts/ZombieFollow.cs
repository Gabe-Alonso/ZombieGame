using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

//This code will ONLY work with a NavMeshAgent attached to the Enemy.
//This code will ONLY work with a NavMesh Surfave in Place.


public class ZombieFollow : MonoBehaviour
{

    public GameObject player;
    public float health = 3;
    private float maxHealth;
    public float timeBetweenHits = 0.5f;

    public Material damagedMaterial;

    private Material _normalMaterial;

    //To keep track of the time since the enemy was last damaged
    private float _damageTimer = 0;
    //This is the variable determining if the enemy CAN take damage again
    private bool _damageBool = true;

    private NavMeshAgent _agent;

    //This is the enemies health bar
    private HealthBar healthBar;

    private void Start()
    {
        //To save the original Material Type
        _normalMaterial = gameObject.GetComponent<Renderer>().material;

        //To get the NavMeshAgent Component
        _agent = GetComponent<NavMeshAgent>();
        maxHealth = health;
        healthBar = GetComponentInChildren<HealthBar>();
        
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        //IF collision is from a BULLET
        if (collision.gameObject.tag == "Bullet")
        {
            if (_damageBool)
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
            }
        }


        //IF Collision is from the PLAYER
        if (collision.gameObject.tag == "Player")
        {
            //Access the Player's Health here some how
            //collision.gameObject.health--;
        }
    }

}
