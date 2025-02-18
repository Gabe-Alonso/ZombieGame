using UnityEngine;
using UnityEngine.AI;

public class w10_Boss : MonoBehaviour
{
    public GameObject zomBarrel;

    public float speed;
    public float acceleration;
    public float health;
    public float throwTime;
    public float throwCooldown;
    public float barrelSpeed;

    public float chargeTime;

    NavMeshAgent _agent;
    ZombieFollow _zomFollow;
    ZombieBarrel _zomBarrel;

    private float _time = 0;
    private float _throwTime = 0;

    private bool _hold = false;
    private GameObject _currBarrel;

    private GameObject _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _zomFollow = GetComponent<ZombieFollow>();
        _agent = GetComponent<NavMeshAgent>();

        _zomFollow.health = health;
        _agent.speed = speed;
        _agent.acceleration = acceleration;

        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _throwTime += Time.deltaTime;

        Throw();
        
    }

    //Spawn in 0/.35/1 locally to the zomb boss
    void Throw()
    {
        if(_throwTime >= throwCooldown + throwTime + .25f) //For the Buffer add .25
        {
            /*_currBarrel = Instantiate(zomBarrel);
            _zomBarrel = _currBarrel.GetComponent<ZombieBarrel>();

            _zomBarrel.throwTimer = throwTime;
            _zomBarrel.transform.parent = transform;


            _currBarrel.transform.rotation = new Quaternion(0, 0f, 90f, 0);
            _currBarrel.transform.position = new Vector3(transform.position.x, transform.position.y + .35f, transform.position.z + 1f);
            
            Rigidbody rb = _currBarrel.GetComponent<Rigidbody>();
            rb.freezeRotation = true;
           
            */
            _currBarrel = Instantiate(zomBarrel, transform.position + new Vector3(0,1,0), Quaternion.identity);
            _currBarrel.transform.LookAt(_player.transform);


            _zomBarrel = _currBarrel.GetComponent<ZombieBarrel>();
            _zomBarrel.speed = barrelSpeed;
            _throwTime = 0;
            _hold = true;
        }
    }


}
