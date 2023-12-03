using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Trace,
        Shoot
    }

    [Header("State")]
    protected bool isIdle;
    protected bool isTrace;
    protected bool isShoot;

    public EnemyState currentState;

    [Header("Stat")]
    protected float MoveSpeed;

    private float currentShootDelay;
    [SerializeField] protected float shootDealy;

    [Header("Component")]
    private Rigidbody rigidBody;

    [SerializeField] protected EnemyRange traceRange;
    [SerializeField] protected EnemyRange shootRange;

    [SerializeField] protected Transform player; 


    void Start()
    {
        InitComponents();

        traceRange.Register(SetTraceState, SetIdleState);
        shootRange.Register(SetShootState, SetTraceState);
    }

    void Update()
    {
        Idle();
        Trace();
        Shoot();
    }

    private void InitComponents() 
    { 
        rigidBody = GetComponent<Rigidbody>();
    }

    // -------------------------------------------------------------------
    private void Idle() 
    {
        if (currentState == EnemyState.Idle) 
        {
            rigidBody.velocity = new Vector3(0,0,0);

            // 이 부분에는 기획에 추가되는 대로 수정할 예정입니다.
        }
    }
    private void Trace() 
    {
        if (currentState == EnemyState.Trace) 
        {

            var dir = player.position - transform.position;
            Vector3 direction = new Vector3(dir.x,rigidBody.velocity.y,dir.z);

            rigidBody.velocity = direction.normalized;

            Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);

            transform.rotation = lookRotation;
        }
    }
    private void Shoot() 
    {
        if (currentState == EnemyState.Shoot)
        {
            rigidBody.velocity = new Vector3(0,0);


            //총알 쏘는 거 넣을 부분
            if (currentShootDelay <= shootDealy)
            {
                currentShootDelay += Time.deltaTime;
                return;
            }
            else 
            {
                Debug.Log("Shoot!");
            }
        }
    }

    // -------------------------------------------------------------------
    private void SetIdleState()
    {
        isIdle = true;
        isTrace = false;
        isShoot = false;

        currentState = EnemyState.Idle;
    }
    private void SetTraceState()
    {
        isIdle = false;
        isTrace = true;
        isShoot = false;

        currentState = EnemyState.Trace;
    }
    private void SetShootState()
    {
        isIdle = false;
        isTrace = false;
        isShoot = true;

        currentState = EnemyState.Shoot;
    }
}
