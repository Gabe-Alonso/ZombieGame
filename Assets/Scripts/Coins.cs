using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public TextMeshProUGUI coins;
    public int numberOfCoins = 0;

    // Update is called once per frame

    public void Awake()
    {
        numberOfCoins = 0;
    }

    public void coinCounterUpdate()
    {
        coins.text = "Coins: $" + numberOfCoins.ToString();
    }


    public void coinCounter(int num)
    {
        numberOfCoins += num;
        coinCounterUpdate();


    }
}