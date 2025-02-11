using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;

public class Enemy : MonoBehaviour
{
    public float enemySpeed;
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    Rigidbody _rb;
    Transform target;
    Vector3 Direction;
    Vector3 angularDirection;
    NavMeshAgent enemy;

    public HealthBar healthBar;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        healthBar = GetComponentInChildren<HealthBar>();
        
    }

    void Start()
    {
        target = GameObject.Find("Player").transform;
        health = 3f;
        maxHealth = health;
        // Debug.Log("health = " + health);
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            //Vector3 direction = (target.position - transform.position).normalized;
            //float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            //angularDirection = new Vector3(0, angle, 0);
            //Direction = new Vector3(direction.x, 0, direction.z);
        }
    }

    private void FixedUpdate()
    {
        if (target != null) { 
            //_rb.linearVelocity = Direction*enemySpeed;
           // _rb.rotation = Quaternion.Euler(angularDirection);
           enemy.destination = target.position;
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        health -= 0.5f;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0f)
        {
            Destroy(gameObject);
         
        }
    }
}
