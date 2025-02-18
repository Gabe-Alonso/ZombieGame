using System.Threading;
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
    }
    void FixedUpdate()
    {
        RotatePlayer();
        MovePlayer();
        if(swapP.IsPressed()){
            KillChildren();
            transform.Find("Pistol").gameObject.SetActive(true);
        } else if(swapS.IsPressed()){
            KillChildren();
            transform.Find("Shotgun").gameObject.SetActive(true);
        } else if(swapAR.IsPressed()){
            KillChildren();
            transform.Find("AR").gameObject.SetActive(true);
        }

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
