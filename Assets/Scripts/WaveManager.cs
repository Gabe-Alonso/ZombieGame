using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public int wave = 1;
    public int zombieWave = 1;
    public GameObject waveComplete;
    public GameObject canvas2;
    public GameObject startNextWave;

    public GameObject shopButton;
    public GameObject shopPrefab;
    public Vector3[] shopSpawns;
    private int _shopIndex = 0;

    public inbetweenWaves inbetweenWaves;


    public TextMeshProUGUI _wavetext;
    [SerializeField] ZombieSpawner _spawner;
    private float _time = 0;
    private bool _roundInactive = false;
    private PlayerMovementScript _player;
    private GameObject _currentShop;

    private int zombieStatueCount = 0;
    void Start()
    {
        Time.timeScale = 1;
        _spawner.spawnZombies(wave);
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
        if (wave == 10)
        {
            canvas2.SetActive(true);
        }
        else
        {
            canvas.SetActive(true);
   
        }
    {
        inbetweenWaves.var = true;
        waveComplete.SetActive(true);
        _time = 3f;
        _roundInactive = true;
    }

    public void nextWaveStart()
    {
        inbetweenWaves.var = false;
        _roundInactive = false;
        startNextWave.SetActive(false);
        waveComplete.SetActive(false);
        SpawnNextShop();
        canvas2.SetActive(false);

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

        if (_shopIndex >= shopSpawns.Length)
        {
            _shopIndex = 0;
        }
        _currentShop = Instantiate(shopPrefab, shopSpawns[_shopIndex], Quaternion.Euler(0, 90, 0));
        _currentShop.transform.Find("shopButtonRange").GetComponent<ShopPopUp>().shopButton = shopButton;
        _shopIndex++;
    }
}
