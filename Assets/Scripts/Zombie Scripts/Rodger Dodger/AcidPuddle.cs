using System.Threading;
using UnityEngine;

public class AcidPuddle : MonoBehaviour
{
    public float despawnTime;

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

        if (_time > despawnTime)
        {
            Destroy(gameObject);
        }
    }

}
