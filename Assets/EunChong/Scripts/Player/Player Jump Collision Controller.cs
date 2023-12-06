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
            Debug.Log("점프업");
        }
        #endregion
    }

    private void OnTriggerStay(Collider other)
    {
        #region JumpDown
        if (other.gameObject.layer == LayerMask.NameToLayer("whatIsGround") &&
            !PlayerMovementController.Instance.grounded)
        {
            if (PlayerMovementController.Instance.isMoving)
            {
                PlayerMovementController.Instance.animator.SetTrigger("JumpOut");
            }

            PlayerMovementController.Instance.grounded = true;
            Debug.Log("점프다운");
        }
        #endregion
    }
}
