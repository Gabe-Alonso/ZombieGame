using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ZombieBarrel : MonoBehaviour
{
    public float throwTimer;
    public float speed;
    public float barrelHealth = 3;
    public GameObject _zomPrefab;

    private float _damageTimer;
    private float _time;
    private Rigidbody _rb;
    private float _damage = 0;
    private bool _damageBool;
    private HealthBar healthBar;
    private float maxHealth;

    public Material damagedMaterial;
    private Material _normalMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _rb = GetComponent<Rigidbody>();
        _damageTimer = 0;
        _damageBool = false;

        maxHealth = barrelHealth;
        healthBar = GetComponentInChildren<HealthBar>();


        _normalMaterial = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

        //Damage Timer, for Time Between Hits
        if (_damageTimer > .25f)
        {
            gameObject.GetComponent<Renderer>().material = _normalMaterial;
            _damageBool = true;
        }
        else
        {
            _damageTimer += Time.deltaTime;
        }

    }
    private void FixedUpdate()
    {
        if (_time >= throwTimer)
        {
            Move();
        }
    }

    void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            if (_damageBool)
            {
                barrelHealth--;
                _damageBool = false;
                _damageTimer = 0;
                gameObject.GetComponent<Renderer>().material = damagedMaterial;
                healthBar.UpdateHealthBar(barrelHealth, maxHealth);
                if (barrelHealth <= 0)
                {
                    Destroy(gameObject);
                }
            }
            
        }
        else if (!(collision.transform.CompareTag("Ground") || collision.transform.CompareTag("Zombie")))
        {
            var zom1 = Instantiate(_zomPrefab, transform.position + new Vector3(.5f, 0, -1.5f), Quaternion.identity);
            var zom2 = Instantiate(_zomPrefab, transform.position + new Vector3(-.5f, 0, -1.5f), Quaternion.identity);
            var zom3 = Instantiate(_zomPrefab, transform.position + new Vector3(0, 0, .5f), Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
