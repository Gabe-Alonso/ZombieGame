using UnityEngine;

public class MuzzleFlashScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float timeVisible;
    void Start()
    {
        timeVisible = 0.1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        timeVisible -= Time.deltaTime;
        if (timeVisible <= 0){
            Destroy(gameObject);
        }
    }
}
