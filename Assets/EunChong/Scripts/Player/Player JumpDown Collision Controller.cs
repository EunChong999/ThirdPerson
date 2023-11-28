using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpDownCollisionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        #region JumpDown
        if (other.gameObject.layer == LayerMask.NameToLayer("whatIsGround") && !PlayerMovementController.Instance.readyToJump)
        {
            PlayerAnimationController.Instance.PlayJumpDownAnim();
        }
        #endregion
    }
}
