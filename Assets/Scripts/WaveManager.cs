using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public int wave = 0;
    public int zombieWave = 0;
    public GameObject waveComplete;
    public GameObject canvas2;
    public GameObject startNextWave;

    public GameObject shopButton;
    public GameObject shopPrefab;
    public Vector3[] shopSpawns;
    private int _shopIndex = 0;
    public GameObject shopCanvas;


    public inbetweenWaves inbetweenWaves;


    public TextMeshProUGUI _wavetext;
    [SerializeField] ZombieSpawner _spawner;
    private float _time = 0;
    private bool _roundInactive = false;
    private PlayerMovementScript _player;
    private GameObject _currentShop;

    public MusicManager AudioManager;

    private int zombieStatueCount = 0;
    void Start()
    {
        Time.timeScale = 1;
        Debug.Log("zombieWave varible is" + zombieWave.ToString());
        _spawner.spawnZombies(zombieWave);
        inbetweenWaves.var = false;

        _player = GameObject.FindWithTag("Player").GetComponent<PlayerMovementScript>();

    }

    private void Update()
    {
        _time -= Time.deltaTime;

        if (_time < 0)
        {
            waveComplete.SetActive(false);
            if (_roundInactive) { startNextWave.SetActive(true); }
        }
    }


    public void PostWaveUI()
    {
        //Reset to Default Music POST_Boss Wave
        AudioManager.PlayDefaultMusic();

        //For Delayed Shop Spawning
        if (wave == 1)
        {
            SpawnNextShop();
        }

        if (wave == 10)
        {
            canvas2.SetActive(true);
        }

        inbetweenWaves.var = true;
        waveComplete.SetActive(true);
        _time = 3f;
        _roundInactive = true;
        
    }

    public void nextWaveStart()
    {
        wave++;

        inbetweenWaves.var = false;
        _roundInactive = false;
        startNextWave.SetActive(false);
        waveComplete.SetActive(false);
        
        //For Delayed Shop Spawning
        if( wave > 1)
        {
            SpawnNextShop();
        }

        canvas2.SetActive(false);

        if (wave % 4 == 0)
        {
            AudioManager.PlayBossMusic();
            BossStart();
            
            _wavetext.text = "Boss";
        }
        else
        {
            zombieWave++;
            _wavetext.text = "Wave " + wave;
            
            _spawner.spawnZombies(zombieWave);
            
            //Spawn a ZombieStatue if the count is above 0
            if (zombieStatueCount != 0)
            {
                _spawner.SpawnZombieStatue(zombieStatueCount);
            }
        }

        //Every 5th Wave, another Zombie Statue will spawn at the start of the round.
        if (wave % 5 == 0) { zombieStatueCount++; }

        
    }

    public void BossStart()
    {
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

    public void SpawnNextShop()
    {

        if (_currentShop != null)
        {
            Destroy(_currentShop);
        }

        if (wave == 1)
        {
            Time.timeScale = 0;
            shopCanvas.SetActive(true);

        }

        if (_shopIndex >= shopSpawns.Length)
        {
            _shopIndex = 0;
        }
        _currentShop = Instantiate(shopPrefab, shopSpawns[_shopIndex], Quaternion.Euler(0, -90, 0));
        _currentShop.transform.Find("shopButtonRange").GetComponent<ShopPopUp>().shopButton = shopButton;
        _shopIndex++;
    }
}
