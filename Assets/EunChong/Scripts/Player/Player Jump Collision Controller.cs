using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpCollisionController : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        #region JumpUp
        if (other.gameObject.layer == LayerMask.NameToLayer("whatIsGround") &&
            PlayerMovementController.Instance.grounded)
        {
            PlayerMovementController.Instance.isLanding = false;
            PlayerMovementController.Instance.grounded = false;
        }
        #endregion
    }

    private void OnTriggerStay(Collider other)
    {
        #region JumpDown
        if (other.gameObject.layer == LayerMask.NameToLayer("whatIsGround"))
        {
            if (!PlayerMovementController.Instance.grounded)
            {
                PlayerMovementController.Instance.animator.SetTrigger("JumpDown");
            }
            else
            {
                // 착지했을 때 이동하거나 웅크릴 경우
                if (PlayerMovementController.Instance.isMoving ||
                    PlayerMovementController.Instance.isCrouching)
                {
                    if (!PlayerMovementController.Instance.isLanding)
                    {
                        PlayerMovementController.Instance.animator.SetTrigger("Land");
                        PlayerMovementController.Instance.isLanding = true;
                    }
                }
            }

            PlayerMovementController.Instance.grounded = true;
        }
        #endregion
    }
}
