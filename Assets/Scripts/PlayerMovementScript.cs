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
        planeCollider = GameObject.Find("Ground").GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        health = 3;
    }
    void FixedUpdate()
    {
        RotatePlayer();
        MovePlayer();
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
