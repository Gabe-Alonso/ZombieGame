using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float timeVisible;
    float timeLeft;
    void Start()
    {
        timeVisible = 0.125f;
        timeLeft = timeVisible;
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0){
            gameObject.SetActive(false);
        }
    }

    public void Activate(){
        timeLeft = timeVisible;
    }
}
