using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    // Update is called once per frame
    void Update()
    {
        // Input

        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
        animator.SetFloat("Speed", rb.velocity.magnitude);

    }

    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            animator.SetTrigger("Up");
        }
        else if (Input.GetKey("s"))
        {
            animator.SetTrigger("Down");
        }
        else if (Input.GetKey("a"))
        {
            animator.SetTrigger("Left");
        }
        else if (Input.GetKey("d"))
        {
            animator.SetTrigger("Right");
        }
    }
}