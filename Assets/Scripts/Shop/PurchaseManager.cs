using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class PurchaseManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Spawner;
    public GameObject broke;

    public GameObject _shotgunSoldOut;
    public GameObject _ARSoldOut;

    public GameObject shotgunIcon;
    public GameObject ARIcon;
    public GameObject GrenadeIcon;


    private PlayerMovementScript _player;
    private PlayerHealth _playerHealth;
    private float _timer = 0;


    //To access the Coins
    private ZombieSpawner _spawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = Player.GetComponent<PlayerMovementScript>();
        _playerHealth = Player.GetComponent<PlayerHealth>();

        _spawner = Spawner.GetComponent<ZombieSpawner>();
        shotgunIcon.SetActive(false);
        ARIcon.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (_timer < Time.realtimeSinceStartup && broke.activeSelf)
        {
            broke.SetActive(false);
        }
    }

    public void InsufficientFunds()
    {
        //Not Enough Money Function
        broke.SetActive(true);
        _timer = Time.realtimeSinceStartup + 2.5f;

    }

    public void PlayerHeal(int price)
    {
        if (_spawner.numberOfCoins >= price)
        {
            _spawner.numberOfCoins -= price;
            _spawner.coinCounterUpdate();

            _playerHealth.health++;
            _playerHealth.UpdateHealth();
        }
        else
        {
            InsufficientFunds();
        }
    }

    public void PlayerMaxHealth(int price)
    {
        if (_spawner.numberOfCoins >= price)
        {
            _spawner.numberOfCoins -= price;
            _spawner.coinCounterUpdate();

            _playerHealth.maxHealth++;
            _playerHealth.UpdateHealth();
        }
        else
        {
            InsufficientFunds();
        }
    }

    public void PlayerSpeed(int price)
    {
        if (_spawner.numberOfCoins >= price)
        {
            _spawner.numberOfCoins -= price;
            _spawner.coinCounterUpdate();

            _player.speed += 1;

        }
        else
        {
            InsufficientFunds();
        }
    }

    public void Shotgun(int price)
    {
        if (_spawner.numberOfCoins >= price)
        {
            _spawner.numberOfCoins -= price;
            _spawner.coinCounterUpdate();

            _player.shotgun = true;
            shotgunIcon.SetActive(true);
            _shotgunSoldOut.SetActive(true);
        }
        else
        {
            InsufficientFunds();
        }
    }

    public void AssultRifle(int price)
    {
        if (_spawner.numberOfCoins >= price)
        {
            _spawner.numberOfCoins -= price;
            _spawner.coinCounterUpdate();

            _player.assultRifle = true;
            ARIcon.SetActive(true);
            _ARSoldOut.SetActive(true);

        }
        else
        {
            InsufficientFunds();
        }
    }

    public void AmmoCrate(int price)
    {
        if (_spawner.numberOfCoins >= price)
        {
            _spawner.numberOfCoins -= price;
            _spawner.coinCounterUpdate();

            _player.AddAmmo(12,2,24);
        }
        else
        {
            InsufficientFunds();
        }
    }

    public void Grenade(int price)
    {
        if (_spawner.numberOfCoins >= price)
        {
            _spawner.numberOfCoins -= price;
            _spawner.coinCounterUpdate();

            _player.grenadeCount += 2;
            GrenadeIcon.SetActive(true);
        }
        else
        {
            InsufficientFunds();
        }
    }
}
