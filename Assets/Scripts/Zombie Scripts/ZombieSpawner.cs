using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    //zombie counter on screen
    public TextMeshProUGUI _zombieCounter;
    [SerializeField] int numberOfZombies;

    //initial boundary arrays 
    private int[] xBoundary = new int[2];
    private int[] zBoundary = new int[2];
    public WaveManager waveManager;

    public GameObject w5Boss;
    public GameObject w10Boss;
 

    private void Awake()
    {
        //_zombieCounter = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void spawnZombies(int wave)
    {
        // Number of zombies increases by 5 per wave
        numberOfZombies = wave * 5;
        updateZombieCounter();

        // Zombie spawn region for wave (will change/expand when we have more waves) 
        if (wave == 1)
        {
            xBoundary[0] = -20;
            xBoundary[1] = 20;
            zBoundary[0] = -20;
            zBoundary[1] = 20;

        }
        else
        {
            xBoundary[0] = -20;
            xBoundary[1] = 20;
            zBoundary[0] = -20;
            zBoundary[1] = 20;

        }

        int i = 0;
        while (i < numberOfZombies)
        {
            // Random spawn position for zombies within x, z bounds (can change later) 

            Vector3 spawnPosition = new Vector3(Random.Range(xBoundary[0], xBoundary[1]), 2.5f, Random.Range(zBoundary[0], zBoundary[1]));
                    
             // Check if this point is inside a NavMeshObstacle
             if (!IsInsideNavMeshObstacle(spawnPosition))
             {
                 Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
                 i++;
                                      
             }
           
        }
    }

    bool IsInsideNavMeshObstacle(Vector3 position)
    {
        NavMeshHit hit;

        // will need to be changed if we ever have floating NavMesh Obstacle
        bool isBlocked = NavMesh.Raycast(position, position +Vector3.up * 2f, out hit, NavMesh.AllAreas);
        return isBlocked;
    }

    public void zombieCounter(int num)
    {
        numberOfZombies += num;
        updateZombieCounter();

        Debug.Log("There are " + numberOfZombies + " zombies left.");
        if (numberOfZombies == 0)
        {
            Time.timeScale = 0;
            waveManager.PostWaveUI();
            
        }
    }

    public void updateZombieCounter()
    {
        _zombieCounter.text = "Zombies Left: " + numberOfZombies.ToString();
    }

    public void spawnBossW5()
    {
        bool spawned = false; 

        while (!spawned)
        {
            // Random spawn position for zombies within x, z bounds (can change later) 

            Vector3 spawnPosition = new Vector3(Random.Range(-20, 20), 2.5f, Random.Range(-20, 20));

            // Check if this point is inside a NavMeshObstacle
            if (!IsInsideNavMeshObstacle(spawnPosition))
            {
                if(Random.Range(0,2) < 1)
                {
                    spawnW10Boss(spawnPosition);
                }
                else
                {
                    spawnW5Boss(spawnPosition);
                }
                numberOfZombies = 1;
                updateZombieCounter();
                spawned = true;

            }

        }
    }

    //To use this, Instatiate a Zombie Boss, then set it equal to this like so
    /*
     * var boss = Instantiate(w5_Boss); 
     * boss = initW5Boss(boss);
    */
    //This will then Instantiate the boss with all the correct number necessarry.
    void spawnW5Boss(Vector3 spawn)
    {
        var boss = Instantiate(w5Boss, spawn, Quaternion.identity);

        boss.GetComponent<w5_Boss>().speed = 7.5f;
        boss.GetComponent<w5_Boss>().acceleration = 10f;
        boss.GetComponent<w5_Boss>().health = 30f;
        boss.GetComponent<w5_Boss>().preChargeTime = 1.5f;
        boss.GetComponent<w5_Boss>().chargeTime = 1.5f;

    }


    //To use this, Instatiate a Zombie Boss, then set it equal to this like so
    /*
     * var boss = Instantiate(w10_Boss); 
     * boss = initW10Boss(boss);
    */
    //This will then Instantiate the boss with all the correct number necessarry.
    void spawnW10Boss(Vector3 spawn)
    {
        var boss = Instantiate(w10Boss, spawn, Quaternion.identity);

        boss.GetComponent<w10_Boss>().speed = 3f;
        boss.GetComponent<w10_Boss>().acceleration = 8f;
        boss.GetComponent<w10_Boss>().health = 40f;
        boss.GetComponent<w10_Boss>().throwTime = 5f;
        boss.GetComponent<w10_Boss>().throwCooldown = 2f;
        boss.GetComponent<w10_Boss>().barrelSpeed = 15f;
        
    }
}
