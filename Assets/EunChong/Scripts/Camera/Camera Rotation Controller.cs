using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationController : MonoBehaviour
{
    [SerializeField] float sensX;
    [SerializeField] float sensY;

    [SerializeField] Transform orientation;

    float mouseX;
    float mouseY;

    float xRotation;
    float yRotation;

    private void Start()
    {
        CursorInit();
    }

    private void CursorInit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MyInput();

        Rotate();
    }

    private void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
    }

    private void Rotate()
    {
        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
