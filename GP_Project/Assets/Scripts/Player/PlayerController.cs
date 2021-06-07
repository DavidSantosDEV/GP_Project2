using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Grounded Stuff")]
    [SerializeField]
    private Transform[] Feet;
    [SerializeField]
    private float raySize=1;
    [SerializeField]
    private LayerMask layerGround;

    private PlayerInput myInput;

    private bool isGrounded=true;
    [SerializeField]
    private bool canMove = true;

    private PlayerMovement _playerMovement;
    private PlayerAnimation _playerAnimation;

    private PlayerWeapon _playerWeapon;
    private PlayerHealth _playerHealth;


    public PlayerHealth PlayerHealthComponent => _playerHealth;
    public PlayerWeapon PlayerWeaponComponent => _playerWeapon;


    private void DebugFunc(InputAction.CallbackContext cntx)
    {
        GameManager.Instance.OpenLevel(0);
    }

    private void Awake()
    {
        myInput = new PlayerInput();

        myInput.Gameplay.Movement.performed += OnMovement;
        myInput.Gameplay.Movement.canceled += OnStopMovement; //Event stuff is way more organized

        myInput.Gameplay.Attack.started += OnAttack;

        myInput.Gameplay.Jump.started += OnJump;

        myInput.Gameplay.Debug.started += DebugFunc;


        myInput.Enable();

        _playerHealth = GetComponent<PlayerHealth>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerWeapon = GetComponent<PlayerWeapon>();
    }

    private void OnMovement(InputAction.CallbackContext cntx)
    {
        if (canMove)
        {
            float val = cntx.ReadValue<float>();
            _playerMovement.UpdateHorizontalInput(val);
            FlipCharacter(val);
            _playerAnimation.UpdateMovementAnimation(Mathf.Abs(val));
        }
    }

    private void OnStopMovement(InputAction.CallbackContext cntx)
    {
        if (canMove)
        {
            StopMovement();
        }
    }

    private void StopMovement()
    {
        _playerMovement.UpdateHorizontalInput(0);
        _playerAnimation.UpdateMovementAnimation(0);
    }

    private void OnJump(InputAction.CallbackContext cntx)
    {
        if (isGrounded)
        {
            //StopMovement();
            _playerMovement.Jump();
        }
    }

    private void OnAttack(InputAction.CallbackContext cntx)
    {
        if (isGrounded)
        {
            StopMovement();
            _playerWeapon.Attack();
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

    //private RaycastHit2D[] aux;

    private void CheckGrounded()
    {
        foreach(Transform t in Feet){
            if(Physics2D.Raycast(t.position, Vector2.down, raySize, layerGround))
            {
                isGrounded = true;
                return;
            }
            else
            {
                isGrounded = false;
            }
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = !isGrounded ?  Color.red: Color.green;
        Gizmos.DrawRay(Feet[0].position, Vector2.down*raySize);
        Gizmos.DrawRay(Feet[1].position, Vector2.down * raySize);
        
    }

    private void FixedUpdate()
    {
        CheckGrounded();
    }

    public void OnDeath()
    {
        enabled = false;
        myInput.Disable();
    }


    public void DisableMovement()
    {
        canMove = false;
    }

    public void EnableMovement()
    {
        canMove = true;
    }


    public void ResetComponents()
    {
        myInput.Enable();
        EnableMovement();
        _playerHealth.ResetComponent();
        _playerWeapon.ResetComponent();
    }
}
