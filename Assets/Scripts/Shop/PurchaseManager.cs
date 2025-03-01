using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Spawner;
    public GameObject broke;

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
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            broke.SetActive(false);
        }
    }

    public void InsufficientFunds()
    {
        //Not Enough Money Function
        broke.SetActive(true);
        _timer = 2.5f;

    }

    public void PlayerDamage(int price)
    {
        if (_spawner.numberOfCoins >= price)
        {

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
}
