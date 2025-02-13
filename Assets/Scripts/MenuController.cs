using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject m_Menu;
    public GameObject button;
    public void OnStartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButton()
    {
        if (m_Menu.activeSelf == true)
        {
            m_Menu.SetActive(!m_Menu.activeSelf);
            button.SetActive(!button.activeSelf);
        }
        else{

            Application.Quit();
        }
    }

    public void OnOptionsButton()
    {
        m_Menu.SetActive(!m_Menu.activeSelf);
        button.SetActive(!button.activeSelf);
    }
}
