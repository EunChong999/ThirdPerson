using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerIKController : MonoBehaviour
{
    [SerializeField] Transform objToLookAt;
    [SerializeField] float maxDistance;
    [SerializeField] float headWeight;
    [SerializeField] float bodyWeight;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        if (Vector3.Distance(transform.position, objToLookAt.position) < maxDistance && 
            PlayerMovementController.Instance.state != PlayerMovementController.State.sprinting)
        {
            anim.SetLookAtPosition(objToLookAt.position);
            anim.SetLookAtWeight(1, bodyWeight, headWeight);
        }
    }
}
