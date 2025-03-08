using UnityEngine;
using UnityEngine.SceneManagement;

public class Returntomenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
