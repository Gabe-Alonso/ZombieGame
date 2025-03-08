using UnityEngine;

public class ShopPopUp : MonoBehaviour
{
    public GameObject shopButton;
    private GameObject _shopCanvas;
    private GameObject _postWaveCanvas;
    private GameObject _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _postWaveCanvas = GameObject.FindWithTag("Post-Wave Screen ");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            shopButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            shopButton.SetActive(false);
            _shopCanvas = GameObject.FindWithTag("ShopCanvas");
            _postWaveCanvas.SetActive(true);

            if (_shopCanvas != null)
            {
                _shopCanvas.SetActive(false);
            }

        }
    }

    private void OnDestroy()
    {
        if (shopButton != null)
            shopButton.SetActive(false);
    }
}
