using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public int wave = 1;
    public int zombieWave = 1;
    public GameObject canvas;
    public Button startWaveButton;
    public Button startBossButton;
    public TextMeshProUGUI _wavetext;
    [SerializeField] ZombieSpawner _spawner;

    private int zombieStatueCount = 0;
    void Start()
    {
        Time.timeScale = 1;
        _spawner.spawnZombies(wave);

    }

    
    public void PostWaveUI()
    {
        canvas.SetActive(true);
    }

    public void nextWaveStart()
    {
        canvas.SetActive(false);

        wave++;
        if (wave % 4 == 0)
        {
            BossStart();
            
            _wavetext.text = "Boss";
        }
        else
        {
            zombieWave++;
            _wavetext.text = "Wave " + wave;
            Time.timeScale = 1;
            _spawner.spawnZombies(zombieWave);
        }

        //Every 4th Wave, another Zombie Statue will spawn at the start of the round.
        if (wave % 5 == 0) { zombieStatueCount++; }

        //Spawn a ZombieStatue if the count is above 0
        if (zombieStatueCount != 0)
        {
            _spawner.SpawnZombieStatue(zombieStatueCount);
        }
    }

    public void BossStart()
    {
        canvas.SetActive(false);
        Time.timeScale = 1;
        
        _spawner.spawnBoss();

    }

    //NO longer neceassarry?
    public void goToShop()
    {
        SceneManager.LoadScene(2);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
