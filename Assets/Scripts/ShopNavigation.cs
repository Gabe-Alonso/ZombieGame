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


    public void Start()
    {
        
    }

    public void Update()
    {
        switch (page)
        {
            case 0:
                if (Page1.activeSelf == true)
                {
                    break;
                }
                else
                {
                    Page1.SetActive(!Page1.activeSelf);
                    Page2.SetActive(!Page2.activeSelf);
                }
                break;

            case 1:
                if (Page1.activeSelf == true){
                    Page2.SetActive(!Page2.activeSelf);
                    Page1.SetActive(!Page1.activeSelf);
                } else if(Page3.activeSelf == true)
                {
                    Page3.SetActive(!Page3.activeSelf);
                    Page2.SetActive(!Page2.activeSelf);
                }

                break;
            case 2:
                if (Page2.activeSelf == true)
                {
                    Page2.SetActive(!Page2.activeSelf);
                    Page1.SetActive(!Page1.activeSelf);
                }
                else if (Page3.activeSelf == true)
                {
                    break;
                }

                
                break;
        }
    }


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
    }

    
}
