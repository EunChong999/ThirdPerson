using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerIKController : MonoBehaviour
{
    [SerializeField] Transform objToLookAt;
    [SerializeField] float headWeight;
    [SerializeField] float bodyWeight;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtPosition(objToLookAt.position);
        anim.SetLookAtWeight(1, bodyWeight, headWeight);
    }
}
