using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpCollisionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        #region JumpUp
        if (other.gameObject.layer == LayerMask.NameToLayer("whatIsGround"))
        {
            PlayerAnimationController.Instance.PlayJumpDownAnim();
        }
        #endregion
    }

    private void OnTriggerExit(Collider other)
    {
        #region JumpDown
        if (other.gameObject.layer == LayerMask.NameToLayer("whatIsGround"))
        {
            PlayerAnimationController.Instance.PlayJumpUpAnim();
        }
        #endregion
    }
}
