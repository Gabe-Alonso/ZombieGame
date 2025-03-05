using TMPro;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefabDefault;
    public GameObject zombiePrefabCharge;
    //zombie counter on screen
    public TextMeshProUGUI _zombieCounter;
    [SerializeField] int numberOfDefaultZombies;
    [SerializeField] int numberOfChargeZombies;
    [SerializeField] int numberOfTotalZombies;

    //initial boundary arrays 
    private int[] xBoundary = new int[2];
    private int[] zBoundary = new int[2];
    public WaveManager waveManager;

    public GameObject w5BossBlock;
    public GameObject w5BossCharge;
    public GameObject w10Boss;
    public GameObject BossHealthBar;
    public GameObject TwinHealthBar;
    public GameObject ZombieStatue;

    public AudioClip BossIntro;
    private AudioSource BossAudioSource;

    public GameObject firstBoss;
    public GameObject firstStatue;

    private GameObject _player;

    private bool _firstBoss = true;
    private bool _firstStatue = true;


    //coin counter
    public TextMeshProUGUI coins;
    public int numberOfCoins = 0;
    private void Awake()
    {
        //_zombieCounter = GetComponentInChildren<TextMeshProUGUI>();
        _player = GameObject.FindWithTag("Player");
    }
    public void spawnZombies(int wave)
    {
        // Number of defaul zombie spawns increase every odd wave
        if (wave % 2 != 0)
        {
            numberOfDefaultZombies = numberOfDefaultZombies + 5;
        }
        else
        {
            numberOfDefaultZombies = 5;
        }
        
        if (wave > 2) 
        { 
            numberOfChargeZombies = wave + 1;
        }
        else
        {
            numberOfChargeZombies = 0;
        }      
        updateZombieCounter();
        numberOfTotalZombies = numberOfDefaultZombies + numberOfChargeZombies;
        // Zombie spawn region for wave (will change/expand when we have more waves) 
        if (wave == 1)
        {
            xBoundary[0] = -20;
            xBoundary[1] = 20;
            zBoundary[0] = -20;
            zBoundary[1] = 20;

        }
        else if (wave == 2)
        {
            xBoundary[0] = -200;
            xBoundary[1] = -160;
            zBoundary[0] = 120;
            zBoundary[1] = 160;

        } 
        else if(wave == 3)
        {
            xBoundary[0] = 250;
            xBoundary[1] = 310;
            zBoundary[0] = -320;
            zBoundary[1] = -270;
        }
        else
        {
            xBoundary[0] = -200;
            xBoundary[1] = -160;
            zBoundary[0] = 120;
            zBoundary[1] = 160;
        }

        if (SceneManager.GetActiveScene().name == "Old Map Playtest" || SceneManager.GetActiveScene().name == "Gannons Testing")
        {
            xBoundary[0] = -50;
            xBoundary[1] = 50;
            zBoundary[0] = -50;
            zBoundary[1] = 50;
        }

        int i = 0;
        while (i < numberOfDefaultZombies)
        {
            // Random spawn position for zombies within x, z bounds (can change later) 

            Vector3 spawnPosition = new Vector3(Random.Range(xBoundary[0], xBoundary[1]), 2.5f, Random.Range(zBoundary[0], zBoundary[1]));
                    
             // Check if this point is inside a NavMeshObstacle
             if (!IsInsideNavMeshObstacle(spawnPosition))
             {
                 Instantiate(zombiePrefabDefault, spawnPosition, Quaternion.identity);
                 i++;
                                      
             }
           
        }

        int ii = 0;
        while (ii < numberOfChargeZombies)
        {
            // Random spawn position for zombies within x, z bounds (can change later) 

            Vector3 spawnPosition = new Vector3(Random.Range(xBoundary[0], xBoundary[1]), 2.5f, Random.Range(zBoundary[0], zBoundary[1]));

            // Check if this point is inside a NavMeshObstacle
            if (!IsInsideNavMeshObstacle(spawnPosition))
            {
                Instantiate(zombiePrefabCharge, spawnPosition, Quaternion.identity);
                ii++;

            }

        }
    }

    bool IsInsideNavMeshObstacle(Vector3 position)
    {
        NavMeshHit hit;

        // will need to be changed if we ever have floating NavMesh Obstacle
        bool isBlocked = NavMesh.Raycast(position, position + Vector3.up * 2f, out hit, NavMesh.AllAreas);
        return isBlocked;
    }

    public void zombieCounter(int num)
    {
        numberOfDefaultZombies += num;
        updateZombieCounter();
        coinCounter(Random.Range(3, 8));

        Debug.Log("There are " + numberOfDefaultZombies + " zombies left.");
        if (numberOfDefaultZombies == 0)
        {
            Time.timeScale = 0;
            waveManager.PostWaveUI();
            
        }
    }


    public void coinCounterUpdate()
    {
        coins.text = "Coins: $" + numberOfCoins.ToString();
    }


    public void coinCounter(int num)
    {
        numberOfCoins += num;
        coinCounterUpdate();


    }

    public void updateZombieCounter()
    {
        numberOfTotalZombies = numberOfDefaultZombies + numberOfChargeZombies;
        _zombieCounter.text = ":" + numberOfTotalZombies.ToString();
    }

    public void spawnBoss()
    {
        bool spawned = false;

        if (_firstBoss)
        {
            _firstBoss = false;
            firstBoss.SetActive(true);
            Time.timeScale = 0;
        }

        while (!spawned)
        {
            var spawnRange = 15;

            // Random spawn position for zombies within x, z bounds (can change later) 
            Vector3 spawnPosition = new Vector3(Random.Range(_player.transform.position.x - spawnRange, _player.transform.position.x + spawnRange), 2.5f, Random.Range(_player.transform.position.z - spawnRange, _player.transform.position.z + spawnRange));

            // Check if this point is inside a NavMeshObstacle
            if (!IsInsideNavMeshObstacle(spawnPosition))
            {
                if(Random.Range(0,2) < 1)
                {
                    spawnW10Boss(spawnPosition);
                    zombieCounter(1);
                }
                else
                {
                    spawnW5Boss(spawnPosition);
                    zombieCounter(2);
                }
                spawned = true;

            }

        }
    }

    //To use this, Instatiate a Zombie Boss, then set it equal to this like so
    //This will then Instantiate the boss with all the correct number necessarry.
    void spawnW5Boss(Vector3 spawn)
    {
        TwinHealthBar.SetActive(true);

        var boss = Instantiate(w5BossBlock, spawn, Quaternion.identity);
        boss.GetComponent<ZombieFollow>().BossHealthBar = TwinHealthBar.transform.GetChild(0).gameObject;
        BossAudioSource = boss.GetComponent<AudioSource>();
        BossAudioSource.PlayOneShot(BossIntro, 1);
        
        var boss2 = Instantiate(w5BossCharge, spawn, Quaternion.identity);
        boss2.GetComponent<ZombieFollow>().BossHealthBar = TwinHealthBar.transform.GetChild(1).gameObject;

    }


    //To use this, Instatiate a Zombie Boss, then set it equal to this like so
    //This will then Instantiate the boss with all the correct number necessarry.
    void spawnW10Boss(Vector3 spawn)
    {
        var boss = Instantiate(w10Boss, spawn, Quaternion.identity);

        boss.GetComponent<ZombieFollow>().BossHealthBar = BossHealthBar;

        
    }


    public void SpawnZombieStatue(int num)
    {
        bool spawned = false;

        var spawnRange = 45f;

        if (_firstStatue)
        {
            _firstStatue = false;
            firstStatue.SetActive(true);
            Time.timeScale = 0;
        }

        while(num > 0)
        {
            while (!spawned)
            {
                // Random spawn position for zombies within x, z bounds (can change later) 
               
                Vector3 spawnPosition = new Vector3(Random.Range(_player.transform.position.x - spawnRange, _player.transform.position.x + spawnRange), 0f, Random.Range(_player.transform.position.z - spawnRange, _player.transform.position.z + spawnRange));

                // Check if this point is inside a NavMeshObstacle
                if (!IsInsideNavMeshObstacle(spawnPosition))
                {
                    var zStatue = Instantiate(ZombieStatue, spawnPosition, Quaternion.identity);
                    zStatue.GetComponent<HealingBoss>().spawner = this;

                    spawned = true;

                }

            }
            num--;
        }
    }
}
