using System.Threading;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovementScript : MonoBehaviour
{

    public Camera cam;
    InputAction moveAction;
    InputAction shootAction;
    InputAction swapP;
    InputAction swapS;
    InputAction swapAR;
    private AudioSource audioSource;
    private Rigidbody rb;
    [SerializeField] float speed;
    public int health;
    RaycastHit hit;
    Collider planeCollider;

    public TextMeshProUGUI _PName;
    public TextMeshProUGUI _SName;
    public TextMeshProUGUI _ARName;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        swapP = InputSystem.actions.FindAction("SelectPistol");
        swapS = InputSystem.actions.FindAction("SelectShotgun");
        swapAR = InputSystem.actions.FindAction("SelectAR");
        planeCollider = GameObject.Find("Ground").GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        health = 3;
        _ARName.enabled = false;
        _SName.enabled = false;
        _PName.enabled = true;
    }
    void FixedUpdate()
    {
        RotatePlayer();
        MovePlayer();
        if(swapP.IsPressed()){
            _ARName.enabled = false;
            _SName.enabled = false;
            _PName.enabled = true;
            KillChildren();
            transform.Find("Pistol").gameObject.SetActive(true);
        } else if(swapS.IsPressed()){
            _ARName.enabled = false;
            _SName.enabled = true;
            _PName.enabled = false;
            KillChildren();
            transform.Find("Shotgun").gameObject.SetActive(true);           
        } else if(swapAR.IsPressed()){
            _ARName.enabled = true;
            _SName.enabled = false;
            _PName.enabled = false;
            KillChildren();
            transform.Find("AR").gameObject.SetActive(true); 
        }

    }

    public void AddAmmo(int pistolAmmo, int shotgunAmmo, int arAmmo){
        transform.Find("Pistol").gameObject.GetComponent<GunScript>().reserveAmmo += pistolAmmo;
        transform.Find("Shotgun").gameObject.GetComponent<GunScript>().reserveAmmo += shotgunAmmo;
        transform.Find("AR").gameObject.GetComponent<GunScript>().reserveAmmo += arAmmo;
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

    
}
