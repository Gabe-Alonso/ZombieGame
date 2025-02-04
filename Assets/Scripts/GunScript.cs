using UnityEngine;

using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public float bulletInterval;
    public float reloadTime;
    private float intervalTimer;
    public GameObject bullet;
    public int maxAmmo;
    private int ammo;
    private float bulletSpeed;
    InputAction shootAction;
    private AudioSource audioSource;
    RaycastHit hit;
    public Camera cam;
    Collider planeCollider;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        shootAction = InputSystem.actions.FindAction("Shoot");
        planeCollider = GameObject.Find("Ground").GetComponent<Collider>();
        reloadTime = 2;
        bulletInterval = 0.2f;
        intervalTimer = 0;
        maxAmmo = 20;
        ammo = maxAmmo;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 mPos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mPos);
        if (Physics.Raycast(ray, out hit)){
            
        }

        if((shootAction.IsPressed()) && (intervalTimer > bulletInterval) && (ammo > 0)){
            shoot();
        }
        if ((ammo == 0) && (intervalTimer > reloadTime)){
            ammo = maxAmmo;
        }
        intervalTimer += Time.fixedDeltaTime;
    }


    private void shoot(){
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        intervalTimer = 0;
        ammo--;
    }
}
