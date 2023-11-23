using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAtController : MonoBehaviour
{
    [SerializeField] Transform camPos;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(camPos.rotation.x, camPos.rotation.y, 0);
    }
}
