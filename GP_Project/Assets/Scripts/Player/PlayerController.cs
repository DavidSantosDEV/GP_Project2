using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour, BoxReferenced
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

    private bool canShoot = true;

    private PlayerMovement _playerMovement;
    private PlayerAnimation _playerAnimation;

    private PlayerWeapon _playerWeapon;
    private PlayerHealth _playerHealth;


    public PlayerHealth PlayerHealthComponent => _playerHealth;
    public PlayerWeapon PlayerWeaponComponent => _playerWeapon;

    private void DebugFunc(InputAction.CallbackContext cntx)
    {
        if(Debug.isDebugBuild)
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

        //SettupParent();
    }

    private void Start()
    {
        AddReference();
    }

    private void OnMovement(InputAction.CallbackContext cntx)
    {
        if (!this) return;
        if (canMove)
        {
            float val = cntx.ReadValue<float>();
            _playerMovement.UpdateHorizontalInput(val);
            if (CheckForFlip(-val))
            {
                Flip();
            }
            //FlipCharacter(val);
            _playerAnimation.UpdateMovementAnimation(Mathf.Abs(val));
        }
    }

    private void OnStopMovement(InputAction.CallbackContext cntx)
    {
        if (this) {
            if (canMove)
            {
                StopMovement();
            }
        }

    }

    private void StopMovement()
    {
        _playerMovement?.UpdateHorizontalInput(0);
        _playerAnimation?.UpdateMovementAnimation(0);
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
        if (isGrounded && canShoot)
        {
            StopMovement();
            _playerWeapon.Attack();
        }
    }


    private bool CheckForFlip(float moveDirection)
    {
        return moveDirection > 0f && transform.right.x < 0f ||
            moveDirection < 0f && transform.right.x > 0f;
    }

    private void Flip()
    {
        Vector3 targetRotation = transform.localEulerAngles;
        targetRotation.y += 180f;
        transform.localEulerAngles = targetRotation;
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
        foreach(Transform foot in Feet)
        {
            Gizmos.DrawRay(foot.position, Vector2.down * raySize);
        }
        //Gizmos.DrawRay(Feet[0].position, Vector2.down*raySize);
        
        
    }

    private void FixedUpdate()
    {
        //Debug.Log("working");
        CheckGrounded();
    }

    public void OnDeath()
    {
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

    public void DeActivateBox()
    {
        canShoot = true;
    }

    public void ActivateBox()
    {
        canShoot = false;
    }

    public void AddReference()
    {
        TheBox[] boxes = FindObjectsOfType<TheBox>();
        foreach (TheBox box in boxes)
        {
            box.AddReference(this);
        }
        //FindObjectOfType<TheBox>().AddReference(this);
    }
}
