using UnityEngine;
using UnityEngine.AI;

public class w5_Boss : MonoBehaviour
{
    public float speed;
    public float health;

    NavMeshAgent _agent;
    ZombieFollow _zomFollow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _zomFollow = GetComponent<ZombieFollow>();
        _agent = GetComponent<NavMeshAgent>();

        _zomFollow.health = health;
        _agent.speed = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
