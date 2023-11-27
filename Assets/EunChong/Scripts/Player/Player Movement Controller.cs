using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement")]

    float moveSpeed;

    [SerializeField] float walkSpeed;
    [SerializeField] float backWalkSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float backSprintSpeed;

    [SerializeField] float groundDrag;

    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;

    bool readyToJump;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask whatIsGround;
    bool grounded;

    [SerializeField] Transform orientation;

    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        ground,
        walking,
        backWalking,
        sprinting,
        backSprinting,
        air
    }

    private static PlayerMovementController instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }

    public static PlayerMovementController Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        GroundCheck();

        MyInput();

        ControlSpeed();

        StateHandler();

        HandleDrag();
    }

    private void GroundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }

    private void HandleDrag()
    {
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ControlSpeed()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        if (horizontalInput != 0 || verticalInput != 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void StateHandler()
    {
        if (grounded && Input.GetKey(sprintKey) && (horizontalInput != 0 || verticalInput != 0))
        {
            if (verticalInput > 0)
            {
                state = MovementState.sprinting;
                moveSpeed = sprintSpeed;
            }
            else if (verticalInput < 0)
            {
                state = MovementState.backSprinting;
                moveSpeed = backSprintSpeed;
            }
        }

        else if (grounded && (horizontalInput != 0 || verticalInput != 0))
        {
            if (verticalInput > 0)
            {
                state = MovementState.walking;
                moveSpeed = walkSpeed;
            }
            else if (verticalInput < 0)
            {
                state = MovementState.backWalking;
                moveSpeed = backWalkSpeed;
            }
        }

        else if (grounded)
        {
            state = MovementState.ground;
        }

        else
        {
            state = MovementState.air;
        }
    }

    private void Move()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        moveDirection.Normalize();

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10 * airMultiplier, ForceMode.Force);
        }
    }
}
