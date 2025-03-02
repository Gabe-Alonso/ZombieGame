using UnityEngine;

public class AcidBall : MonoBehaviour
{
    public float duration;
    public float speed;
    public GameObject acidPuddle;

    private float _time;
     

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

        if (_time > duration)
        {
            placeAcid();
        }
        else
        {
            Move();
        }
    }

    void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!(collision.transform.CompareTag("Zombie")))
        {
            placeAcid();
        }

    }

    private void placeAcid()
    {

        var acid = Instantiate(acidPuddle, new Vector3(transform.position.x, .1f, transform.position.z), Quaternion.identity);

        Destroy(gameObject);
    }
}
