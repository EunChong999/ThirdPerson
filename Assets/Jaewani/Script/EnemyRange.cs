using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyRange : MonoBehaviour
{

    public float rangeScale;
    private void Start() => transform.localScale = new Vector3(rangeScale, rangeScale, rangeScale);

    public void Register(Action enterAction, Action exitAction)
    {
        triggerEnterAction = enterAction;
        triggerExitAction = exitAction;
    }

    public Action triggerEnterAction;
    public Action triggerExitAction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) triggerEnterAction.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) triggerExitAction.Invoke();
    }
}
