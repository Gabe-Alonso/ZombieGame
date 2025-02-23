using UnityEngine;
using UnityEngine.AI;

public class w5_Boss : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float health;

    public float preChargeTime;
    public float chargeTime;

    NavMeshAgent _agent;
    ZombieFollow _zomFollow;

    private float _time = 0;
    private float _chargeTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _zomFollow = GetComponent<ZombieFollow>();
        _agent = GetComponent<NavMeshAgent>();

        _zomFollow.health = health;
        _agent.speed = speed;
        _agent.acceleration = acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        _chargeTime += Time.deltaTime;

        if (_chargeTime >= chargeTime + preChargeTime + .25f) //For the Buffer add .25
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

            _chargeTime = 0;
            _time = 0;
        }


        
    }
}
