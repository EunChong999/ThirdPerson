using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovementController;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator anim;

    public bool walk;
    public bool sprint;

    private void Update()
    {
        ManageAnimState();
    }

    private void ManageAnimState()
    {
        #region Walk
        if (PlayerMovementController.Instance.state == MovementState.walking)
        {
            PlayWalkAnim(true);
        }
        else
        {
            PlayWalkAnim(false);
        }
        #endregion

        #region BackWalk
        if (PlayerMovementController.Instance.state == MovementState.backWalking)
        {
            PlayBackWalkAnim(true);
        }
        else
        {
            PlayBackWalkAnim(false);
        }
        #endregion

        #region Sprint
        if (PlayerMovementController.Instance.state == MovementState.sprinting)
        {
            PlaySprintAnim(true);
        }
        else
        {
            PlaySprintAnim(false);
        }
        #endregion

        #region BackSprint
        if (PlayerMovementController.Instance.state == MovementState.backSprinting)
        {
            PlayBackSprintAnim(true);
        }
        else
        {
            PlayBackSprintAnim(false);
        }
        #endregion
    }

    private void PlayWalkAnim(bool isWalk)
    {
        if (isWalk)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    private void PlayBackWalkAnim(bool isBackWalk)
    {
        if (isBackWalk)
        {
            anim.SetBool("BackWalk", true);
        }
        else
        {
            anim.SetBool("BackWalk", false);
        }
    }

    private void PlaySprintAnim(bool isSprint)
    {
        if (isSprint)
        {
            anim.SetBool("Sprint", true);
        }
        else
        {
            anim.SetBool("Sprint", false);
        }
    }

    private void PlayBackSprintAnim(bool isBackSprint)
    {
        if (isBackSprint)
        {
            anim.SetBool("BackSprint", true);
        }
        else
        {
            anim.SetBool("BackSprint", false);
        }
    }
}
