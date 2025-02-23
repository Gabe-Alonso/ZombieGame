using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    private float maxHealth;
    public float timeBetweenHits = 0.5f;

    public Material damagedMaterial;

    private Material _normalMaterial;

    //To keep track of the time since the enemy was last damaged
    private float _damageTimer = 0;
    //This is the variable determining if the enemy CAN take damage again
    private bool _damageBool = true;

    //This is the players health bar
    private HealthBar healthBar;

    void Start()
    {
        //To save the original Material Type
        _normalMaterial = gameObject.GetComponent<Renderer>().material;

        //To get the NavMeshAgent Component
        
        maxHealth = health;
        healthBar = GetComponentInChildren<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
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
        //IF collision is from a Zombie
        if (collision.gameObject.tag == "Zombie")
        {
            if (_damageBool)
            {
                health--;
                _damageBool = false;
                _damageTimer = 0;
                gameObject.GetComponent<Renderer>().material = damagedMaterial;
                healthBar.UpdateHealthBar(health, maxHealth);
            }
           
        }


       
    }
}
