using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject flash;
    public GameObject damageRadius;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player"){
            Explode();
        }
    }
    
    public void Explode(){
        GameObject mf = Instantiate(flash, transform.position, Quaternion.identity);
        mf.transform.Rotate(0, 90, 0);
        mf.transform.localScale *= 0.1f;
        Instantiate(damageRadius, transform.position, Quaternion.identity);
        //Instantiate(damageRadius, transform.position, Quaternion.identity);
        //Instantiate(damageRadius, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
