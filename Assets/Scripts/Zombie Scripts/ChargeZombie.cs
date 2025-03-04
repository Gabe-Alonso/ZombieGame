using UnityEngine;
using UnityEngine.AI;

public class ChargeZombie : MonoBehaviour
{
    private float speed = 7f;
    private float acceleration = 10f;
    private float health = 3f;

    public float preChargeTime;
    public float chargeTime;

    NavMeshAgent _agent;
    ZombieFollow _zomFollow;

    private float _time = 0;
    private float _chargeTime = 0;
    private bool _enraged = false;

    void Start()
    {
        _zomFollow = GetComponent<ZombieFollow>();
        _zomFollow.isBoss = true;

        _agent = GetComponent<NavMeshAgent>();

        _zomFollow.health = health;
        _agent.speed = speed;
        _agent.acceleration = acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        _chargeTime += Time.deltaTime;

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

    void Charge()
    {
        if (_time < preChargeTime)
        {
            _agent.speed = 0;
            _agent.acceleration = 0f;
            _agent.velocity = Vector3.zero;
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
