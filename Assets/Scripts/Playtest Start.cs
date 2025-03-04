using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaytestStart : MonoBehaviour
{
   public void LoadMainMap()
    {
        SceneManager.LoadScene("Backup Terrain");
    }

    public void LoadPlayTestMap()
    {
        SceneManager.LoadScene("Old Map Playtest");
    }
}
