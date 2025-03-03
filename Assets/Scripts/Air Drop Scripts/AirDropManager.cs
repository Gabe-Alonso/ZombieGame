using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class AirDropManager : MonoBehaviour
{
    public Airdrop airDrop;
    public float spawnTime;
    public float despawnTime;
    public GameObject firstTimePrompt;

    public float spawnRange = 50;


    private GameObject _player;

    //Play Test Spawn Points
    public Vector3[] spawnArray = { };

    public bool randomSpawns;

    private float _time = 0;
    private float _duration = 0;
    private Airdrop _spawned = null;

    private bool _first = true;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
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
        else
        {
            _duration += Time.deltaTime;

            if (_duration >= despawnTime)
            {
                Destroy(_spawned.gameObject);
                _duration = 0;
            }
        }

        if (Time.timeScale == 0 && _spawned)
        {
            Destroy(_spawned.gameObject);
            _time = 0;
            _duration = 0;
        }

    }

    bool IsInsideNavMeshObstacle(Vector3 position)
    {
        NavMeshHit hit;

        // will need to be changed if we ever have floating NavMesh Obstacle
        bool isBlocked = NavMesh.Raycast(position, position + Vector3.up * 2f, out hit, NavMesh.AllAreas);

        return isBlocked;
    }

    public void SpawnAirDrop()
    {
        if (_first)
        {
            _first = false;
            firstTimePrompt.SetActive(true);
            Time.timeScale = 0;
        }

        if (randomSpawns)
        {
            while (_spawned == null)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(_player.transform.position.x - spawnRange, _player.transform.position.x + spawnRange), 2.25f, Random.Range(_player.transform.position.z - spawnRange, _player.transform.position.z + spawnRange));
                Debug.Log("trying to spawn Airdrop");

                // Check if this point is inside a NavMeshObstacle
                if (!IsInsideNavMeshObstacle(spawnPosition))
                {
                    _spawned = Instantiate(airDrop, spawnPosition, Quaternion.identity);

                }
            }
               
        }
        else
        {
            _spawned = Instantiate(airDrop, spawnArray[Random.Range(0, spawnArray.Length)], Quaternion.identity);
        }


       
    }

    public void Unfreeze()
    {
        Time.timeScale = 1;
    }
}
