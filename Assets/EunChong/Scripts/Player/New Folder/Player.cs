//플레이어블 캐릭터
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        //키입력 받기
        KeyboardInput();

        //매프레임 실행해야하는 동작 호출.
        stateMachine.DoOperateUpdate();
    }

    //키보드 입력
    void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //달리기, 슬라이딩 중일 때만 점프 가능
            stateMachine.SetState(dicState[PlayerState.Jump]);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            //달리기 중에만 슬라이딩 가능.
            if (stateMachine.CurrentState == dicState[PlayerState.Sprint])
            {
                stateMachine.SetState(dicState[PlayerState.Slide]);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            stateMachine.SetState(dicState[PlayerState.Dead]);
        }
    }
}