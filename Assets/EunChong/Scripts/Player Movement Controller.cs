using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float turnSmoothTime;

    float turnSmoothVelocity;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothTime, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            controller.Move(direction * walkSpeed * Time.deltaTime);
        }
    }
}
