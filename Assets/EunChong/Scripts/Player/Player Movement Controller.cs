using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement")]

    float moveSpeed;

    [SerializeField] float walkSpeed;
    [SerializeField] float slowWalkSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float slowSprintSpeed;

    [SerializeField] float groundDrag;

    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;

    [HideInInspector] public bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask whatIsGround;
    [HideInInspector] public bool grounded;

    [SerializeField] Transform orientation;

    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;

    Vector3 moveDirection;

    [HideInInspector] public Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        ground,
        walking,
        sprinting,
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
            state = MovementState.sprinting;

            if (verticalInput > 0)
            {
                moveSpeed = sprintSpeed;
            }
            else if (verticalInput < 0 || horizontalInput != 0)
            {
                moveSpeed = slowSprintSpeed;
            }
            else
            {
                moveSpeed = sprintSpeed;
            }
        }

        else if (grounded && (horizontalInput != 0 || verticalInput != 0))
        {
            state = MovementState.walking;

            if (verticalInput > 0)
            {
                moveSpeed = walkSpeed;
            }
            else if (verticalInput < 0 || horizontalInput != 0)
            {
                moveSpeed = slowWalkSpeed;
            }
            else
            {
                moveSpeed = walkSpeed;
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
