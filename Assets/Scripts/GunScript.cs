using UnityEngine;

using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public float bulletInterval;
    public float reloadTime;
    bool isReloading;
    private float intervalTimer;
    public GameObject bullet;
    public int maxAmmo;
    private int ammo;
    private float bulletSpeed;
    InputAction shootAction;
    InputAction reloadAction;
    private AudioSource audioSource;
    RaycastHit hit;
    public Camera cam;
    Collider planeCollider;
    int totalAmmo;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        shootAction = InputSystem.actions.FindAction("Shoot");
        reloadAction = InputSystem.actions.FindAction("Reload");
        planeCollider = GameObject.Find("Ground").GetComponent<Collider>();
        reloadTime = 2;
        isReloading = false;
        bulletInterval = 0.2f;
        intervalTimer = 0;
        totalAmmo = 100;
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
        
        if((shootAction.IsPressed()) && (!isReloading) && (intervalTimer > bulletInterval) && (ammo > 0)){
            shoot();
            Debug.Log("Current Clip: " + ammo);
        }
        if (((ammo == 0) || reloadAction.IsPressed()) && (!isReloading) && (ammo < maxAmmo)){
            Debug.Log("Reloading...");
            isReloading = true;
            if (maxAmmo > totalAmmo) {
                
                ammo = totalAmmo;
            } else if (totalAmmo == 0) {
                Debug.Log("Cant Reload, out of ammo");
            } else if (ammo > 0) {
                totalAmmo -= maxAmmo - ammo;
                ammo = maxAmmo;
            } else {
                ammo = maxAmmo;
                totalAmmo -= maxAmmo;
            }
            intervalTimer = 0;
            Debug.Log("Total Ammo: " + totalAmmo);
            Debug.Log("Current Clip: " + ammo);
        }
        intervalTimer += Time.fixedDeltaTime;
        if (intervalTimer > reloadTime){
            isReloading = false;
        }
    }


    private void shoot(){
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        intervalTimer = 0;
        ammo--;
    }
}
