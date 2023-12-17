using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private Dialogue d;
    public GameObject dialogueBox;
    public GameObject text;
    private Animator animator;
    private bool playerIsNear = false;
    private int index = 0;
    private float transitionTime = 0.5f;
    private bool confirmEnd = false;
    private bool dialogueStart = false;

    void Start()
    {
        dialogueManager = GameObject.FindWithTag("DialogueManager").GetComponent<DialogueManager>();
        d = gameObject.GetComponentInParent<Dialogue>();
        animator = dialogueBox.GetComponent<Animator>();
    }

    void Update()
    {
        // identifying whether dialogue is in progress
        if (dialogueManager.inProgress)
        {
            animator.SetBool("InProgress", true);
        }
        else
        {
            if (animator.gameObject.activeSelf)
            {
                animator.SetBool("InProgress", false);
            }
        }

        if (animator.gameObject.activeSelf)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("PaperBox_InProgress"))
            {
                dialogueStart = true;
            }
        }

        if (playerIsNear && Input.GetKeyDown(KeyCode.E))
        {

            //this is just STARTING the animation if its off... first time
            if (!dialogueBox.activeSelf)
            {
                text.SetActive(true);
                dialogueBox.SetActive(true);
                animator.SetTrigger("Start");

            }
            // this is when the animation has already started... dialogue in progress
            else
            {
                //last line
                if (index == d.lines.Length - 1)
                {
                    dialogueManager.textSpeed = 0f;
                    if (!dialogueManager.isTyping)
                    {
                        confirmEnd = true;
                        StartCoroutine(EndAnimation());
                    }
                }
            }

            // PRINTING TEXT
            if (confirmEnd)
            {
                index = 0;
                dialogueStart = false;
            }
            else
            {
                // IF STILL TYPING, JUST FINISH LINE... ELSE START THE NEXT LINE
                if (dialogueManager.isTyping)
                {
                    dialogueManager.textSpeed = 0f;
                }
                else
                {
                    // TO PREVENT DIALOGUE FROM CONTINUING WHILE BOX IS CLOSING
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PaperBox_End") || !animator.GetCurrentAnimatorStateInfo(0).IsName("PaperBox_Start"))
                    {
                        if (dialogueStart)
                        {
                            index++;
                        }
                        dialogueManager.StartDialogue(d, animator, index);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            playerIsNear = false;

            // disable dialogueBox
            if (!playerIsNear)
            {
                StartCoroutine(EndAnimation());
            }
        }
    }

    IEnumerator EndAnimation()
    {
        // DISACTIVE TEXT
        text.SetActive(false);

        if (animator.gameObject.activeSelf)
        {
            animator.SetTrigger("End");
        }
        d.textComponent.text = string.Empty;
        yield return new WaitForSeconds(transitionTime);

        dialogueManager.inProgress = false;
        index = 0;
        confirmEnd = false;
        dialogueStart = false;
        dialogueBox.SetActive(false);
    }
}
