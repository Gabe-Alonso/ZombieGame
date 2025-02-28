using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public int wave;
    public GameObject canvas;
    public Button startWaveButton;
    public Button startBossButton;
    [SerializeField] ZombieSpawner _spawner;
    void Start()
    {
        wave = 1;
        _spawner.spawnZombies(wave);
       // canvas.enabled = false;

    }

    
    public void PostWaveUI()
    {
        canvas.SetActive(true);
    }

    public void nextWaveStart()
    {
        canvas.SetActive(false);

        if (wave % 3 == 0)
        {
            BossStart();
        }
        else
        {
            wave++;
            Time.timeScale = 1;
            _spawner.spawnZombies(wave);
        }
    }

    public void BossStart()
    {
        canvas.SetActive(false);
        Time.timeScale = 1;
        
        _spawner.spawnBossW5();

    }

    public void goToShop()
    {
        SceneManager.LoadScene(2);
    }
}
