using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States
{
    public class StateCllimbing : IState
    {
        public void OperateEnter()
        {

        }

        public void OperateExit()
        {

        }

        public void OperateUpdate()
        {

        }
    }

    public class StateCrouching : IState
    {
        public void OperateEnter()
        {

        }

        public void OperateExit()
        {

        }

        public void OperateUpdate()
        {

        }
    }

    public class StateDeading : IState
    {
        public void OperateEnter()
        {

        }

        public void OperateExit()
        {

        }

        public void OperateUpdate()
        {

        }
    }

    public class StateJumping : IState
    {
        public void OperateEnter()
        {
            PlayerMovementController.Instance.animator.SetTrigger("Jump");
        }

        public void OperateExit()
        {

        }

        public void OperateUpdate()
        {

        }
    }

    public class StateRolling : IState
    {
        public void OperateEnter()
        {

        }

        public void OperateExit()
        {

        }

        public void OperateUpdate()
        {

        }
    }

    public class StateSliding : IState
    {
        public void OperateEnter()
        {

        }

        public void OperateExit()
        {

        }

        public void OperateUpdate()
        {

        }
    }

    public class StateSprinting : IState
    {
        public void OperateEnter()
        {
            PlayerMovementController.Instance.animator.SetBool("Sprint", true);
        }

        public void OperateExit()
        {
            PlayerMovementController.Instance.animator.SetBool("Sprint", false);
        }

        public void OperateUpdate()
        {

        }
    }

    public class StateStanding : IState
    {
        public void OperateEnter()
        {

        }

        public void OperateExit()
        {

        }

        public void OperateUpdate()
        {

        }
    }

    public class StateWalking : IState
    {
        public void OperateEnter()
        {
            PlayerMovementController.Instance.animator.SetBool("Walk", true);
        }

        public void OperateExit()
        {
            PlayerMovementController.Instance.animator.SetBool("Walk", false);
        }

        public void OperateUpdate()
        {

        }
    }

}





