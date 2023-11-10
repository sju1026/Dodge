using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 180f;

    private PlayerInput pI;
    private Rigidbody rb;
    private Animator anim;

    void Start()
    {
        pI = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate();
        Move();

        anim.SetFloat("Move", pI.move);
    }

    private void Move()
    {
        Vector3 moveDistance = pI.move * transform.forward * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDistance);
    }

    private void Rotate()
    {
        float turn = pI.rotate * rotateSpeed * Time.fixedDeltaTime;
        rb.rotation = rb.rotation * Quaternion.Euler(0, turn, 0);
    }
}
