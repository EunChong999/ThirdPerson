using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpUpCollisionController : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        #region JumpUp
        if (other.gameObject.layer == LayerMask.NameToLayer("whatIsGround"))
        {
            PlayerAnimationController.Instance.PlayJumpUpAnim();
        }
        #endregion
    }
}
