using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovementController;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator anim;

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

    }

    private void PlayWalkAnim(bool isWalk)
    {
        if (isWalk)
        {
            
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

        }
        else
        {
            anim.SetBool("Sprint", false);
        }
    }
}
