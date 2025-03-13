using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturntoMenu : MonoBehaviour
{
    public void goToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
