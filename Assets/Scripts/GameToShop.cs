using UnityEngine;
using static Unity.Collections.AllocatorManager;
using UnityEngine.SceneManagement;

public class GameToShop : MonoBehaviour
{

    //public AudioSource click;

    public void OnShopButton()
    {
       // click.Play();
        SceneManager.LoadScene(2);

    }
}
