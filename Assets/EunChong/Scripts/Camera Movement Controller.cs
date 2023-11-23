using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField] Transform camPos;

    private void Update()
    {
        CamPosInit();
    }

    private void CamPosInit()
    {
        transform.position = camPos.position;
    }
}
