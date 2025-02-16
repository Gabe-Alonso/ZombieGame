using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int wave;
    [SerializeField] ZombieSpawner _spawner;
    void Start()
    {
        wave = 1;
        _spawner.spawnZombies(wave);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
