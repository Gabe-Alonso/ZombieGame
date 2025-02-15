using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    [SerializeField] int numberOfZombies;
    private int[] xBoundary = new int[2];
    private int[] zBoundary = new int[2]; 
    
    public void spawnZombies(int wave)
    {
        // Number of zombies increases by 5 per wave
        numberOfZombies = wave * 5; 
        
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

        for (int i = 0; i < numberOfZombies; i++)
        {
            // Random spawn position for zombies within x, z bounds (can change later) 

            Vector3 spawnPosition = new Vector3(Random.Range(xBoundary[0], xBoundary[1]), 2.5f, Random.Range(zBoundary[0], zBoundary[1]));
            Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void zombieCounterDecrement()
    {
        numberOfZombies--;
        Debug.Log("There are " + numberOfZombies + " zombies left.");
        if (numberOfZombies == 0)
        {
            Time.timeScale = 0;
            
        }
    }
}
