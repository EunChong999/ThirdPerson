using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateJumping : IState
{
    public void OperateEnter()
    {
        PlayerAnimationController.Instance.anim.SetTrigger("Jump");
    }

    public void OperateExit()
    {

    }

    public void OperateUpdate()
    {

    }
}
