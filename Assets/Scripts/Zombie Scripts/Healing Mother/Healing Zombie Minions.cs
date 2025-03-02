using UnityEngine;
using UnityEngine.AI;

public class HealingZombieMinions : MonoBehaviour
{
    public GameObject Mother;
    
    public float speed;
    public float acceleration;
    public float health;

    private float _time;

    NavMeshAgent _agent;
    ZombieFollow _zomFollow;

    private GameObject _player;
    private bool run = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _zomFollow = GetComponent<ZombieFollow>();

        _agent = GetComponent<NavMeshAgent>();

        _zomFollow.tracking = false;

        _zomFollow.health = health;
        _agent.speed = speed;
        _agent.acceleration = acceleration;

        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mother != null)
        {
            if (_zomFollow.health <= 2)
            {
                run = true;
            }

                if (run)
                {
                    _agent.destination = Mother.transform.position;
                    _agent.acceleration = 3 * acceleration;

                    if (GetDistanceFromMother() < 5)
                    {
                        _time += Time.deltaTime;
                        if (_time > 1f)
                        {
                            _zomFollow.health++;
                            _time = 0f;
                            _zomFollow.UpdateHealth();
                        }
                        if (_zomFollow.health == _zomFollow.maxHealth)
                        {
                            _agent.acceleration = acceleration;
                            run = false;
                        }
                    }
                }
                else
                {
                    _time = 0;
                    _agent.destination = _player.transform.position;
                }
        }
        _agent.destination = _player.transform.position;
        


    }

    public float GetDistanceFromMother()
    {
        return Mathf.Abs(Vector3.Distance(Mother.transform.position, transform.position));
    }
}
