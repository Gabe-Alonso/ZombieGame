using UnityEngine;

public class AirDropManager : MonoBehaviour
{
    public Airdrop airDrop;
    public float spawnTime = 5;

    private float _time = 0;
    private Airdrop _spawned;

    //Play Test Spawn Points
    public Vector3[] spawnArray = { new Vector3(45, 1.25f, -45), new Vector3(37.5f, 1.25f, 20), new Vector3(-42.5f, 1.25f, 42.5f) };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        if (_spawned == null)
        {
            _time += Time.deltaTime;
            if (_time >= spawnTime)
            {
                SpawnAirDrop();
                _time = 0;
            }
        }

    }

    public void SpawnAirDrop()
    {
        _spawned = Instantiate(airDrop, spawnArray[Random.Range(0, 3)], Quaternion.identity);

    }
}
