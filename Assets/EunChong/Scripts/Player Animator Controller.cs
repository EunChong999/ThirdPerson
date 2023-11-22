using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private bool walk;
    [SerializeField] private bool run;
    [SerializeField] private bool walkToStop;
    [SerializeField] private bool runToStop;

    private void Update()
    {
        ManageAnimState();
    }

    private void ManageAnimState()
    {
        #region Walk
        if (walk)
        {
            PlayWalkAnim(true);
        }
        else
        {
            PlayWalkAnim(false);
        }
        #endregion

        #region Run
        if (run)
        {
            PlayRunAnim(true);
        }
        else
        {
            PlayRunAnim(false);
        }
        #endregion

        #region WalkToStop
        if (walkToStop)
        {
            PlayWalkToStopAnim();
        }
        #endregion

        #region RunToStop
        if (runToStop)
        {
            PlayRunToStopAnim();
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

    private void PlayRunAnim(bool isRun)
    {
        if (isRun)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    private void PlayWalkToStopAnim()
    {
        anim.SetTrigger("Walk To Stop");

        walkToStop = false;
    }

    private void PlayRunToStopAnim()
    {
        anim.SetTrigger("Run To Stop");

        runToStop = false;
    }
}
