using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Spawner;

    private PlayerMovementScript _player;
    private PlayerHealth _playerHealth;

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
        
    }

    public void InsufficientFunds()
    {
        //Not Enough Money Function
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
}
