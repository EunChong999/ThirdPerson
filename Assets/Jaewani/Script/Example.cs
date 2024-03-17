using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    private Rigidbody Rb;
    public float MoveSpeed;
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Rb.velocity = new Vector3(x,Rb.velocity.y,z).normalized * MoveSpeed;
    }
}
