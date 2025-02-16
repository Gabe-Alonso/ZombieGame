using UnityEngine;
using UnityEngine.AI;

public class w10_Boss : MonoBehaviour
{
    public GameObject zomBarrel;

    public float speed;
    public float acceleration;
    public float health;

    public float preChargeTime;
    public float chargeTime;

    NavMeshAgent _agent;
    ZombieFollow _zomFollow;

    private float _time = 0;
    private float _throwTime = 0;

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
        _throwTime += Time.deltaTime;

        if (_throwTime >= chargeTime + preChargeTime + .25f) //For the Buffer add .25
        {
            _time += Time.deltaTime;
            Throw();
        }
    }

    //Spawn in 0/.35/1 locally to the zomb boss
    void Throw()
    {
        var newBarrel = Instantiate(zomBarrel);

        newBarrel.transform.position = new Vector3(transform.position.x, transform.position.y + .35f, transform.position.z + 1f);

    }

}
