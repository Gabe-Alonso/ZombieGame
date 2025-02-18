using UnityEngine;
using static Unity.Collections.AllocatorManager;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Collections;

public class ShopNavigation : MonoBehaviour
{
    public AudioSource click;
    public GameObject Page1;
    public GameObject Page2;
    public GameObject Page3;

    public float page = 0;
        



    public void OnReturnButton()
    {
        click.Play();
        SceneManager.LoadScene(1);

    }

    public void OnNextButton()
    {
        if (page >= 2)
        {
            page = 2;
        }
        else
        {


            page += 1;
        }


        switch (page)
        {
            case 0:

                break;

            case 1:

                Page1.SetActive(!Page1.activeSelf);//off
                Page2.SetActive(!Page2.activeSelf);//on
                
                break;

            case 2:

                Page2.SetActive(!Page2.activeSelf); //off
                Page3.SetActive(!Page3.activeSelf); //on

                break;
        }
    }

    public void OnPreviousButton() 
    {
        if (page == 0)
        {
            page = 0;
        } else
        {
            page -= 1;
        }

        switch (page)
        {
            case 0:

                Page1.SetActive(!Page1.activeSelf);//on
                Page2.SetActive(!Page2.activeSelf);//off

                break;

            case 1:

                Page2.SetActive(!Page2.activeSelf); //on
                Page3.SetActive(!Page3.activeSelf); //off
                
                break;
            case 2:

                break;
        }
    }

    
}

