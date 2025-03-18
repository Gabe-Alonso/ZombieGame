using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float timeActive;
    void Start()
    {
        timeActive = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        timeActive -= Time.deltaTime;
        if (timeActive <= 0){
            Destroy(gameObject);
        }
    }
}
