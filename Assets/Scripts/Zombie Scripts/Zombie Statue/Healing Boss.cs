using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class HealingBoss : MonoBehaviour
{
    public GameObject healingZombie;

    public float health;
    public float spawnTimer = 10;

    private float _time;
    private HealthBar healthBar;
    private float maxHealth;
    public ZombieSpawner spawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //To Account for the Spawner
        spawner.zombieCounter(1);
        maxHealth = health;
        healthBar = GetComponentInChildren<HealthBar>();

        //Instantiate 3 Zombies to start
        SpawnZombie(3);
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        //Every 10 seconds Instantiate another Zombie
        if (_time >= spawnTimer)
        {
            SpawnZombie(1);
            _time = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //IF collision is from a BULLET
        if (collision.gameObject.tag == "Bullet")
        {
            health--;
            healthBar.UpdateHealthBar(health, maxHealth);

            if (health <= 0)
            {
                Destroy(this.gameObject);
            }

        }
    }

    public bool IsOnNavMesh(Vector3 position)
    {
        NavMeshHit hit;
        bool isOnNavMesh = NavMesh.SamplePosition(position, out hit, 10f, NavMesh.AllAreas);
        if (isOnNavMesh)
        {
            return true;
        }
        else
        {
            Debug.Log("NavMesh Surface Not Found");
            return true;
        }
    } 
    bool IsInsideNavMeshObstacle(Vector3 position)
    {
        NavMeshHit hit;

        // will need to be changed if we ever have floating NavMesh Obstacle
        bool isBlocked = NavMesh.Raycast(position, position + Vector3.up * 2f, out hit, NavMesh.AllAreas);
        return isBlocked;
    }

    void SpawnZombie(int num)
    {
        int i = 0;
        while (i < num)
        {
            // Random spawn position for zombies within x, z bounds (can change later) 

            Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x - 5, transform.position.x + 5), 2.5f, Random.Range(transform.position.z - 5, transform.position.z + 5));

            // Check if this point is inside a NavMeshObstacle
            if (!IsInsideNavMeshObstacle(spawnPosition) && IsOnNavMesh(spawnPosition))
            {

                var spawned = Instantiate(healingZombie, spawnPosition, Quaternion.identity);
                spawned.GetComponent<HealingZombieMinions>().Mother = this.gameObject;

                spawner.zombieCounter(1);
                i++;

            }

        }
    }

    private void OnDestroy()
    {
        spawner.zombieCounter(-1);
    }


}
