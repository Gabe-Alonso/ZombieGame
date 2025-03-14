using System;
using TMPro;
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
    public int reserveAmmo;
    public bool isShotgun;
    public int shotgunShots;
    public float despawnDist;
    public TextMeshProUGUI _ammoCount;
    public GameObject muzzleFlash;

    public inbetweenWaves inbetweenWaves;

    private AudioSource _audio;
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        shootAction = InputSystem.actions.FindAction("Shoot");
        reloadAction = InputSystem.actions.FindAction("Reload");
        planeCollider = GameObject.Find("Ground").GetComponent<Collider>();
        //reloadTime = 2;
        isReloading = false;
        //bulletInterval = 0.2f;
        intervalTimer = 0;
        //totalAmmo = 10000; //comment out with 100
        //maxAmmo = 20;
        ammo = maxAmmo;
        shotgunShots = 5;
        _ammoCount.text = ":" + ammo.ToString() + "/" + maxAmmo.ToString() + "/" + reserveAmmo.ToString();
        _audio = GetComponent<AudioSource>();
        
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 mPos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mPos);
        if (Physics.Raycast(ray, out hit)){
        }
        
        if((shootAction.IsPressed()) && (!isReloading) && (intervalTimer > bulletInterval) && (ammo > 0) && !inbetweenWaves.var){
            shoot();
            GameObject mf = Instantiate(muzzleFlash, transform.position, Quaternion.identity);
            _audio.Play();
            mf.transform.SetParent(gameObject.transform);
            //mf.transform.position += new Vector3(0,0,1);
            mf.transform.Rotate(0, 90, 0);
            mf.transform.localScale *= 0.1f;

            Debug.Log("Current Clip: " + ammo);
            _ammoCount.text = ":" + ammo.ToString() + "/" + maxAmmo.ToString() + "/" + reserveAmmo.ToString();
        }
        if (((ammo == 0) || reloadAction.IsPressed()) && (!isReloading) && (ammo < maxAmmo) && (reserveAmmo > 0)){
            Debug.Log("Reloading...");
            _ammoCount.text = "Reloading...";
            
            
            isReloading = true;
            if (maxAmmo > reserveAmmo) {
                
                ammo = reserveAmmo;
                reserveAmmo = 0;
            } else if (reserveAmmo == 0) {
                Debug.Log("Cant Reload, out of ammo");
                _ammoCount.text = "Out of Ammo";

            } else if (ammo > 0) {
                reserveAmmo -= maxAmmo - ammo;
                ammo = maxAmmo;
            } else {
                ammo = maxAmmo;
                reserveAmmo -= ammo;
            }
            intervalTimer = 0;
            Debug.Log("Total Ammo: " + reserveAmmo);
            Debug.Log("Current Clip: " + ammo);
        }
        intervalTimer += Time.fixedDeltaTime;
        if (intervalTimer > reloadTime){
            isReloading = false;
            if (reserveAmmo == 0 && ammo == 0){
                _ammoCount.text = "Out of Ammo";
            }else{
                _ammoCount.text = ":" + ammo.ToString() + "/" + maxAmmo.ToString() + "/" + reserveAmmo.ToString();
            }
            
        }
    }

    public void UpdateAmmo()
    {
        _ammoCount.text = ":" + ammo.ToString() + "/" + maxAmmo.ToString() + "/" + reserveAmmo.ToString();
    }


    private void shoot(){
        if (isShotgun){
            for(int i = 0; i < shotgunShots; i++){
                GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                newBullet.GetComponent<BulletScript>().despawnDist = despawnDist;
                newBullet.GetComponent<BulletScript>().isPiercing = true;
                newBullet.transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }
        }else{
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            newBullet.GetComponent<BulletScript>().despawnDist = despawnDist;
            newBullet.transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            

        }
        
        intervalTimer = 0;
        ammo--;
    }
}
