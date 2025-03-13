using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    GameObject w10Cutscene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayW10Cutscene()
    {
        w10Cutscene.SetActive(true);
    }
}
