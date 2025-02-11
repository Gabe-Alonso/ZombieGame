using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;

public class Enemy : MonoBehaviour
{
    public float enemySpeed;
   
    Rigidbody _rb;
    Transform target;
    Vector3 Direction;
    Vector3 angularDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            angularDirection = new Vector3(0, angle, 0);
           
            Direction = new Vector3(direction.x, 0, direction.z);
        }
    }

    private void FixedUpdate()
    {
        if (target != null) { 
            _rb.linearVelocity = Direction*enemySpeed;
            _rb.rotation = Quaternion.Euler(angularDirection);
        }
    }
}
