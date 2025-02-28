using Unity.VisualScripting;
using UnityEngine;

public class Airdrop : MonoBehaviour
{
    public Zone zone;
    public float captureRadius;
    public float captureTime;

    private float _time;
    private GameObject _player;
    private Zone _zone;


    private HealthBar timerBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _time = 0;

        _zone = Instantiate(zone, new Vector3(transform.position.x, .1f, transform.position.z), Quaternion.identity);
        _zone.transform.localScale = new Vector3(captureRadius*2, _zone.transform.localScale.y, captureRadius*2);

        timerBar = GetComponentInChildren<HealthBar>();
        timerBar.UpdateHealthBar(captureTime, captureTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (_zone.playerInZone)
        {
            _time += Time.deltaTime;
            timerBar.UpdateHealthBar(captureTime - _time, captureTime);

            if (_time > captureTime)
            {
                openAirDrop();
                Destroy(gameObject);
            }
        }
    }

    //Air Drop HERE
    void openAirDrop()
    {

        _player.GetComponent<PlayerMovementScript>().AddAmmo(12 * Random.Range(1, 3), 2 * Random.Range(1, 3), 24 * Random.Range(1, 3));
        _player.GetComponent<PlayerHealth>().AddHealth(Random.Range(1,3));
    }
    private void OnDestroy()
    {
        if( _zone )
            Destroy(_zone.gameObject);
    }

    //When spawn in, create an arrow on the Player's UI pointing Towards the air Drop, if the airdrop is On Screen, do not display the arrow

    //If the Player is within a given Radius of the Airdop (show by a circle on the ground) The Airdrop's progress bar begins depleting. Once
    //the bar is fully complete, the airdrop disapears and gives the Player whatever was in it.

}
