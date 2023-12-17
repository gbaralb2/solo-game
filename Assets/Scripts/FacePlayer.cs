using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    private Collider2D col;
    private Animator anim;
    private SpriteRenderer sr;
    void Start() 
    {
        col = gameObject.GetComponent<Collider2D>();
        anim = gameObject.GetComponentInParent<Animator>();
        sr = gameObject.GetComponentInParent<SpriteRenderer>();
    }
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) > 
            Mathf.Abs(player.transform.position.y - gameObject.transform.position.y))
            {
                if (player.transform.position.x < gameObject.transform.position.x)
                {
                    anim.SetTrigger("LookLeft");
                }
                else
                {
                    anim.SetTrigger("LookRight");
                }
            }
            else
            {
                if (player.transform.position.y < gameObject.transform.position.y)
                {
                    anim.SetTrigger("LookDown");
                }
                else
                {
                    anim.SetTrigger("LookUp");
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            foreach (var trigger in anim.parameters)
            {
                if (trigger.type == AnimatorControllerParameterType.Trigger)
                {
                    anim.ResetTrigger(trigger.name);
                }
            }
            anim.SetTrigger("OutOfRange");
        }
    }
}
