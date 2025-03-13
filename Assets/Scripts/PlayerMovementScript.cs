using System.Collections;
using System.Threading;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class PlayerMovementScript : MonoBehaviour
{

    public Camera cam;
    InputAction moveAction;
    InputAction shootAction;
    InputAction swapP;
    InputAction swapS;
    InputAction swapAR;
    InputAction sprintAction;
    
    // Haungs Mode Data
    InputAction haungsAction;
    bool haungsModeOn;
    Dictionary<string, object> preHaungsData = new Dictionary<string, object>();


    // Variables for dash
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 30f;
    private float dashingTime = 0.1f;
    private float dashingCooldown = 10f;
    public Slider dashSlider;
    private float sliderTimer;
    [SerializeField] private TrailRenderer trail;

    private AudioSource audioSource;
    private Rigidbody rb;
    public float speed;
    public int health;
    RaycastHit hit;
    Collider planeCollider;

    public bool shotgun = false;
    public bool assultRifle = false;

    public TextMeshProUGUI _PName;
    public TextMeshProUGUI _SName;
    public TextMeshProUGUI _ARName;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        swapP = InputSystem.actions.FindAction("SelectPistol");
        swapS = InputSystem.actions.FindAction("SelectShotgun");
        swapAR = InputSystem.actions.FindAction("SelectAR");
        haungsAction = InputSystem.actions.FindAction("Haungs");
        planeCollider = GameObject.Find("Ground").GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        health = 3;
        _ARName.enabled = false;
        _SName.enabled = false;
        _PName.enabled = true;

        sliderTimer = dashingCooldown;
        haungsModeOn = false;

    }
    void FixedUpdate()
    {
        RotatePlayer();
        MovePlayer();
        StartCoroutine(Dash());
        if(swapP.IsPressed()){
            _ARName.enabled = false;
            _SName.enabled = false;
            _PName.enabled = true;
            KillChildren();
            transform.Find("Pistol").gameObject.SetActive(true);
        } else if(swapS.IsPressed() && shotgun){
            _ARName.enabled = false;
            _SName.enabled = true;
            _PName.enabled = false;
            KillChildren();
            transform.Find("Shotgun").gameObject.SetActive(true);           
        } else if(swapAR.IsPressed() && assultRifle){
            _ARName.enabled = true;
            _SName.enabled = false;
            _PName.enabled = false;
            KillChildren();
            transform.Find("AR").gameObject.SetActive(true); 
        }
        

    }

    void Update()
    {
        if(haungsAction.WasPressedThisFrame()){
            haungsModeOn = !haungsModeOn;
            Debug.Log("Haungs Mode: " + haungsModeOn);
            haungsToggle();
        }
    }

    public void haungsToggle(){
        if(haungsModeOn){
            preHaungsData.Add("shotgunStatus", shotgun);
            preHaungsData.Add("ARStatus", assultRifle);
            preHaungsData.Add("health", gameObject.GetComponent<PlayerHealth>().health);
            preHaungsData.Add("pistolData", transform.Find("Pistol").gameObject.GetComponent<GunScript>().reserveAmmo);
            preHaungsData.Add("pistolReload", transform.Find("Pistol").gameObject.GetComponent<GunScript>().reloadTime);
            
            preHaungsData.Add("shotgunData", transform.Find("Shotgun").gameObject.GetComponent<GunScript>().reserveAmmo);
            preHaungsData.Add("shotgunReload", transform.Find("Shotgun").gameObject.GetComponent<GunScript>().reloadTime);
        
            preHaungsData.Add("ARData", transform.Find("AR").gameObject.GetComponent<GunScript>().reserveAmmo);
            preHaungsData.Add("ARReload", transform.Find("AR").gameObject.GetComponent<GunScript>().reloadTime);
            preHaungsData.Add("dashCooldown", dashingCooldown);

            shotgun = true;
            assultRifle = true;
            gameObject.GetComponent<PlayerHealth>().health = 10000;
            transform.Find("Pistol").gameObject.GetComponent<GunScript>().reserveAmmo = 10000;
            transform.Find("Pistol").gameObject.GetComponent<GunScript>().reloadTime = 0;
            transform.Find("Shotgun").gameObject.GetComponent<GunScript>().reserveAmmo = 10000;
            transform.Find("Shotgun").gameObject.GetComponent<GunScript>().reloadTime = 0;
            transform.Find("AR").gameObject.GetComponent<GunScript>().reserveAmmo = 10000;
            transform.Find("AR").gameObject.GetComponent<GunScript>().reloadTime = 0;
            dashingCooldown = 0f;

        }else{
            shotgun = (bool)preHaungsData["shotgunStatus"];
            assultRifle = (bool)preHaungsData["ARStatus"];
            gameObject.GetComponent<PlayerHealth>().health = (float)preHaungsData["health"];
            transform.Find("Pistol").gameObject.GetComponent<GunScript>().reserveAmmo = (int)preHaungsData["pistolData"];
            transform.Find("Pistol").gameObject.GetComponent<GunScript>().reloadTime = (float)preHaungsData["pistolReload"];
            transform.Find("Shotgun").gameObject.GetComponent<GunScript>().reserveAmmo = (int)preHaungsData["shotgunData"];
            transform.Find("Shotgun").gameObject.GetComponent<GunScript>().reloadTime = (float)preHaungsData["shotgunReload"];
            transform.Find("AR").gameObject.GetComponent<GunScript>().reserveAmmo = (int)preHaungsData["ARData"];
            transform.Find("AR").gameObject.GetComponent<GunScript>().reloadTime = (float)preHaungsData["ARReload"];
            dashingCooldown = (float)preHaungsData["dashCooldown"];
            preHaungsData.Clear();

            _ARName.enabled = false;
            _SName.enabled = false;
            _PName.enabled = true;
            KillChildren();
            transform.Find("Pistol").gameObject.SetActive(true);
            

        }
    }

    public void AddAmmo(int pistolAmmo, int shotgunAmmo, int arAmmo){
        transform.Find("Pistol").gameObject.GetComponent<GunScript>().reserveAmmo += pistolAmmo;
        if(shotgun)
            transform.Find("Shotgun").gameObject.GetComponent<GunScript>().reserveAmmo += shotgunAmmo;
        if(assultRifle)
            transform.Find("AR").gameObject.GetComponent<GunScript>().reserveAmmo += arAmmo;

        UpdateAmmo();
    }

    public void UpdateAmmo()
    {
        transform.Find("Pistol").gameObject.GetComponent<GunScript>().UpdateAmmo();
        transform.Find("Shotgun").gameObject.GetComponent<GunScript>().UpdateAmmo();
        transform.Find("AR").gameObject.GetComponent<GunScript>().UpdateAmmo();
    }

    private void KillChildren(){
        foreach (Transform child in transform){
            if(child.gameObject.transform.CompareTag("Gun")){
                child.gameObject.SetActive(false);
            }
        }           
    }

    private void RotatePlayer(){
        Vector2 mPos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mPos);
        if (Physics.Raycast(ray, out hit)){
            if(hit.collider == planeCollider){
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }
        }
    }

    private void MovePlayer(){
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 newPosition = transform.position + new Vector3(moveInput.x, 0, moveInput.y) * speed * Time.deltaTime;
        transform.position = newPosition;
    }

    private IEnumerator Dash()
    {
        if (sprintAction.IsPressed() && canDash)
        {
            Debug.Log("Dash function is running");
            canDash = false;
            isDashing = true;
            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            rb.linearVelocity = new Vector3(moveInput.x, 0, moveInput.y)*dashingPower;
            trail.emitting = true;
            yield return new WaitForSeconds(dashingTime);
            dashSlider.value = 1f;
            sliderTimer = dashingCooldown;
            rb.linearVelocity = new Vector3(0, 0, 0);
            trail.emitting = false;
            isDashing = false;

            while (sliderTimer > 0)
            {
                sliderTimer -= Time.deltaTime;
                dashSlider.value = sliderTimer / dashingCooldown; // Scale slider between 0 and 1
                yield return null; // Wait for next frame
            }

            dashSlider.value = 0f; // Ensure slider is empty at the end
            canDash = true;
        }

    }
    
}
