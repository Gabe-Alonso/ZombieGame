using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject m_Menu;
    public GameObject button;
    public AudioSource click;
    public void OnStartButton()
    {
        click.Play();
        SceneManager.LoadScene(1);
        
    }

    public void OnQuitButton()
    {
        if (m_Menu.activeSelf == true)
        {
            click.Play();
            m_Menu.SetActive(!m_Menu.activeSelf);
            button.SetActive(!button.activeSelf);
          
        }
        else{

            click.Play();
            Application.Quit();
        }
    }

    public void OnOptionsButton()
    {
        click.Play();
        m_Menu.SetActive(!m_Menu.activeSelf);
        button.SetActive(!button.activeSelf);
    
    }
}
