using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;

public class w5_Block_Boss : MonoBehaviour
{
    public GameObject acidBall;

    public float speed;
    public float acceleration;
    public float health;
    public float throwCooldown;
    public float acidSpeed;

    NavMeshAgent _agent;
    ZombieFollow _zomFollow;
    AcidBall _acid;

    private float _throwTime = 0;

    private GameObject _acidBall;
    private GameObject _player;

    private bool _enraged;

    /* EVENT TO COMMUNICATE WITH OTHER BOSS */
    public delegate void BlockBossDead();
    public static event BlockBossDead Enrage;

    private void OnDestroy()
    {
        if (!_enraged)
            Enrage();
    }

    void OnEnable()
    {
        w5_Boss.Enrage += StartRage;
    }

    void OnDisable()
    {
        w5_Boss.Enrage -= StartRage;
    }

    void StartRage()
    {
        _enraged = true;
        _agent.speed = speed * 1.5f;
        _agent.acceleration = acceleration * 1.5f;

        throwCooldown = .25f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _zomFollow = GetComponent<ZombieFollow>();
        _zomFollow.isBoss = true;

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

        if (_zomFollow.GetDistanceFromPlayer() <= 5)
        {
            _zomFollow.runAway = true;
        }
        else
        {
            _zomFollow.runAway = false;
        }

        if (_throwTime >= throwCooldown + .25f) //For the Buffer add .25
        {
            ThrowAcid();
            _throwTime = 0;

        }
    }


    //Spawn in 0/.35/1 locally to the zomb boss
    public void ThrowAcid()
    {
            _acidBall = Instantiate(acidBall, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            _acidBall.transform.LookAt(_player.transform);

            _acid = _acidBall.GetComponent<AcidBall>();
            
            _acid.speed = acidSpeed;
    }


}
