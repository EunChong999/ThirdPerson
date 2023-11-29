using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovementController;

public class PlayerJumpDownCollisionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        #region JumpDown
        if (other.gameObject.layer == LayerMask.NameToLayer("whatIsGround") && PlayerMovementController.Instance.state == MovementState.ground)  
        {
            PlayerAnimationController.Instance.PlayJumpDownAnim();
        }
        #endregion
    }
}
