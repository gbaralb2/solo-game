using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerStay2D(Collider2D player)
    {
        animator.SetBool("PlayerIsNear", true);
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        animator.SetBool("PlayerIsNear", false);
    }
}
