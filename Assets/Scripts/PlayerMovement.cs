using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    void Update()
    {
        animator.SetFloat("Speed", rb.velocity.magnitude);

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

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
    }
}
