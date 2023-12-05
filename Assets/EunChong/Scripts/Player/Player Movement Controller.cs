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

    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask whatIsGround;
    public bool grounded;

    [SerializeField] Transform orientation;

    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;

    Vector3 moveDirection;

    [HideInInspector] public Rigidbody rb;

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

    private enum PlayerState
    {
        Stand,
        Walk,
        Sprint,
        Jump,
        Crouch,
        Slide,
        Roll,
        Climb,
        Dead
    }

    private StateMachine stateMachine;

    //스테이트들을 보관
    private Dictionary<PlayerState, IState> dicState = new Dictionary<PlayerState, IState>();

    // Start is called before the first frame update
    void Start()
    {
        RbInit();
        StateInit();
    }

    private void RbInit()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void StateInit()
    {
        //상태 생성
        IState stand = new StateStanding();
        IState walk = new StateWalking();
        IState sprint = new StateSprinting();
        IState jump = new StateJumping();
        IState crouch = new StateCrouching();
        IState slide = new StateSliding();
        IState roll = new StateRolling();
        IState climb = new StateCllimbing();
        IState dead = new StateDeading();

        //키입력 등에 따라서 언제나 상태를 꺼내 쓸 수 있게 딕셔너리에 보관
        dicState.Add(PlayerState.Stand, stand);
        dicState.Add(PlayerState.Walk, walk);
        dicState.Add(PlayerState.Sprint, sprint);
        dicState.Add(PlayerState.Jump, jump);
        dicState.Add(PlayerState.Crouch, crouch);
        dicState.Add(PlayerState.Slide, slide);
        dicState.Add(PlayerState.Roll, roll);
        dicState.Add(PlayerState.Climb, climb);
        dicState.Add(PlayerState.Dead, dead);

        //기본상태는 달리기로 설정.
        stateMachine = new StateMachine(stand);
    }

    private void Update()
    {
        //매프레임 실행해야하는 동작 호출.
        stateMachine.DoOperateUpdate();

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

        isMoving = horizontalInput != 0 || verticalInput != 0;

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            stateMachine.SetState(dicState[PlayerState.Jump]);

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
        if (isMoving)
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
        stateMachine.SetState(dicState[PlayerState.Stand]);

        readyToJump = true;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void StateHandler()
    {
        if (grounded && Input.GetKey(sprintKey) && (isMoving))
        {
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

        else if (grounded && (isMoving))
        {
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
    }

    private void Move()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

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
