using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionController : MonoBehaviour
{
    [SerializeField] Transform camPos;

    private void Update()
    {
        CamPose();
    }

    private void CamPose()
    {
        transform.position = camPos.position;
    }
}
