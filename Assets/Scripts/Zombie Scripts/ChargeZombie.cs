using UnityEngine;
using UnityEngine.AI;

public class ChargeZombie : MonoBehaviour
{
    [SerializeField] private float speed = 11f;
    [SerializeField] private float acceleration = 10f;


    private float preChargeTime = 0.5f;
    private float chargeTime;

    NavMeshAgent _agent;
    ZombieFollow _zomFollow;
    private GameObject player;

    private float _time = 0;
    private float _chargeTime = 0;
    private bool _enraged = false;

    private float distanceToPlayer;

    void Start()
    {
        _zomFollow = GetComponent<ZombieFollow>();
        _zomFollow.isBoss = true;

        _agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");


        _agent.speed = speed;
        _agent.acceleration = acceleration;
        _zomFollow.enabled = true;

        chargeTime = Random.Range(0.7f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        // calculate distance to play sound at volume 
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        _chargeTime += Time.deltaTime;

        if (distanceToPlayer <= 20)
        {
            _zomFollow.enabled = false;
            if (_chargeTime >= chargeTime + preChargeTime + .25f && !_enraged) //For the Buffer add .25
            {
                _time += Time.deltaTime;
                Charge();
            }
            else if (_enraged && _chargeTime >= .25f)
            {
                _time += Time.deltaTime;
                Charge();
            }
        }
        else
        {
            _zomFollow.enabled = true;
            _agent.speed = speed;
            _agent.acceleration = acceleration;
        }
    }

    void Charge()
    {
        if (_time < preChargeTime)
        {
            _agent.speed = 0;
            _agent.acceleration = 0f;
            _agent.velocity = Vector3.zero;
            _agent.destination = player.transform.position;
        }
        else if (_time >= preChargeTime && _time < chargeTime + preChargeTime)
        {
            _agent.speed = 100f;
            _agent.acceleration = 50f;
        }
        else
        {
            _agent.velocity = Vector3.zero;
            //Set the speed and Acceleration back
            _agent.speed = speed;
            _agent.acceleration = acceleration;

            if (_enraged)
            {
                chargeTime = Random.Range(.25f, 1.5f);
            }

            _chargeTime = 0;
            _time = 0;
        }
    }
}
