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
            PlayerMovementController.Instance.grounded = false;
        }
        #endregion
    }

    private void OnTriggerStay(Collider other)
    {
        #region JumpDown
        if (other.gameObject.layer == LayerMask.NameToLayer("whatIsGround") &&
            !PlayerMovementController.Instance.grounded)
        {
            // 착지했을 때 이동할 경우
            if (PlayerMovementController.Instance.isMoving)
            {
                PlayerMovementController.Instance.animator.SetTrigger("Land");
            }

            PlayerMovementController.Instance.grounded = true;
        }
        #endregion
    }
}
