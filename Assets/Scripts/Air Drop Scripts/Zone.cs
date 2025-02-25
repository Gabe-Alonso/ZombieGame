using UnityEngine;

public class Zone : MonoBehaviour
{
    public Material outOfZone;
    public Material inTheZone;

    public bool playerInZone = false;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        { 
            playerInZone = true;
            gameObject.GetComponent<Renderer>().material = inTheZone;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInZone = false;
            gameObject.GetComponent<Renderer>().material = outOfZone;
        }
    }
}
