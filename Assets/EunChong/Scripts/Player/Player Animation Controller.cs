using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovementController;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator anim;

    private static PlayerAnimationController instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }

    public static PlayerAnimationController Instance
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

    private void Update()
    {
        ManageAnimState();
    }

    private void ManageAnimState()
    {
        anim.SetFloat("Horizontal", PlayerMovementController.Instance.horizontalInput);
        anim.SetFloat("Vertical", PlayerMovementController.Instance.verticalInput);

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

        #region JumpLoop
        if (PlayerMovementController.Instance.state == MovementState.air)
        {
            PlayJumpLoopAnim(true);
        }
        else
        {
            PlayJumpLoopAnim(false);
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

    public void PlayJumpUpAnim()
    {
        anim.SetTrigger("JumpUp");
    }

    private void PlayJumpLoopAnim(bool isJumpLoop)
    {
        if (isJumpLoop)
        {
            anim.SetBool("JumpLoop", true);
        }
        else
        {
            anim.SetBool("JumpLoop", false);
        }
    }

    public void PlayJumpDownAnim()
    {
        anim.SetTrigger("JumpDown");
    }
}
