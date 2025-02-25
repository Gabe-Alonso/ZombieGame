using UnityEngine;
using static Unity.Collections.AllocatorManager;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameToShop : MonoBehaviour
{
    public GameObject Shop;
    public AudioSource click;


    public void OnShopButton()
    {
        click.Play();
        Shop.SetActive(!Shop.activeSelf);
    }
}
