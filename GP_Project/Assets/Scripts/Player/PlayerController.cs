using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Grounded Stuff")]
    [SerializeField]
    private Transform Foot;
    [SerializeField]
    private float raySize=1;
    [SerializeField]
    private LayerMask layerGround;

    [SerializeField]
    private float speedGroundedCheck=2;

    private PlayerInput myInput;

    private bool isGrounded=true;

    private PlayerMovement _playerMovement;
    private void Awake()
    {
        myInput = new PlayerInput();

        myInput.Gameplay.Movement.performed += OnMovement;
        myInput.Gameplay.Movement.canceled += OnStopMovement; //Event stuff is way more organized

        myInput.Gameplay.Attack.started += OnAttack;

        myInput.Gameplay.Jump.started += OnJump;


        myInput.Enable();

        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnMovement(InputAction.CallbackContext cntx)
    {
        float val= cntx.ReadValue<float>();
        _playerMovement.UpdateHorizontalInput(val);
        FlipCharacter(val);

    }

    private void OnStopMovement(InputAction.CallbackContext cntx)
    {
        _playerMovement.UpdateHorizontalInput(0);
    }

    private void OnJump(InputAction.CallbackContext cntx)
    {
        if (isGrounded)
        {
            _playerMovement.Jump();
        }
    }

    private void OnAttack(InputAction.CallbackContext cntx)
    {
        if (isGrounded)
        {

        }
    }

    private void FlipCharacter(float val)
    {
        if (val > 0)
        {
            transform.localEulerAngles = new Vector2(transform.localEulerAngles.x, 180);
        }
        else
        {
            transform.localEulerAngles = new Vector2(transform.localEulerAngles.x, 0);
        }

    }

    void Start()
    {
        
    }

    //private RaycastHit2D[] aux;

    private void CheckGrounded()
    {
        isGrounded = Physics2D.Raycast(Foot.position, Vector2.down, raySize, layerGround);
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = !isGrounded ?  Color.red: Color.green;
        Gizmos.DrawRay(Foot.position, Vector2.down*raySize);
        
    }

    private void FixedUpdate()
    {
        CheckGrounded();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
