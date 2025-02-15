using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    //zombie counter on screen
    private TextMeshProUGUI _zombieCounter;
    [SerializeField] int numberOfZombies;
    private int[] xBoundary = new int[2];
    private int[] zBoundary = new int[2];
    public NavMeshSurface ground;

    private void Awake()
    {
        _zombieCounter = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void spawnZombies(int wave)
    {
        // Number of zombies increases by 5 per wave
        numberOfZombies = wave * 5; 
        _zombieCounter.text = "Zombies Left: " + numberOfZombies.ToString();
        
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
            NavMeshHit hit;

            if (NavMesh.SamplePosition(spawnPosition, out hit, 2f, NavMesh.AllAreas))
            {
                // Check if this point is inside a NavMeshObstacle
                if (!IsInsideNavMeshObstacle(hit.position))
                {
                    Instantiate(zombiePrefab, hit.position, Quaternion.identity);
                    i++;
                   
                    
                }
            }
        }
    }

    bool IsInsideNavMeshObstacle(Vector3 position)
    {
        NavMeshHit hit;

        // will need to be changed if we ever have floating NavMesh Obstacle
        bool isBlocked = NavMesh.Raycast(position + Vector3.up * 2f, position - Vector3.up * 2f, out hit, NavMesh.AllAreas);
        return isBlocked;
    }

    public void zombieCounterDecrement()
    {
        numberOfZombies--;
        _zombieCounter.text = "Zombies Left: " + numberOfZombies.ToString();
        Debug.Log("There are " + numberOfZombies + " zombies left.");
        if (numberOfZombies == 0)
        {
            Time.timeScale = 0;
            
        }
    }
}
