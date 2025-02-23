using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    Vector3 initialPosition;
    public float despawnDist;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        despawnDist = 30;
        // rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (Vector3.Distance(initialPosition, transform.position) > despawnDist){
            Destroy(gameObject);
        }
        //rb.linearVelocity = transform.right * speed;
    }

    private void OnCollisionEnter(Collision collision){
        
        if (!collision.transform.CompareTag("Player")){
            Destroy(gameObject);
        }
    }
}