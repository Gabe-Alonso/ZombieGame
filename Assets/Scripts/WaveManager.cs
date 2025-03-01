using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public int wave = 0;
    public GameObject canvas;
    public Button startWaveButton;
    public Button startBossButton;
    public TextMeshProUGUI _wavetext;
    [SerializeField] ZombieSpawner _spawner;
    void Start()
    {
        Time.timeScale = 1;
        wave = 1;
        _spawner.spawnBoss();
        //_spawner.spawnZombies(wave);
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
            _wavetext.text = "Boss";
            wave++;
        }
        else
        {
            wave++;
            _wavetext.text = "Wave " + wave;
            Time.timeScale = 1;
            _spawner.spawnZombies(wave);
        }
    }

    public void BossStart()
    {
        canvas.SetActive(false);
        Time.timeScale = 1;
        
        _spawner.spawnBoss();

    }

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
