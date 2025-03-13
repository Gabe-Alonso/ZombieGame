using UnityEngine;

public class BloodScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    ParticleSystem bloodPS;
    void Start()
    {
        bloodPS = gameObject.GetComponent<ParticleSystem>();
        if (bloodPS != null)
        {
            bloodPS.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!bloodPS.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
