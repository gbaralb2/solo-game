using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    public float textSpeed = 0.03f;
    public bool inProgress;
    public bool isTyping;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
 
    public void StartDialogue(Dialogue d, Animator animator, int index)
    {
        d.textComponent.text = string.Empty;
        if (!inProgress)
        {
            inProgress = true;
            if (index == 0)
            {
                StartCoroutine(WaitForBox(d, animator, index));
            }
            else
            {
                StartCoroutine(TypeLine(d, animator, index));
            }
        }
        else
        {
            NextLine(d, animator, index);
        }
    }

    void NextLine(Dialogue d,Animator animator, int index)
    {
        StartCoroutine(TypeLine(d, animator, index));
    }

    IEnumerator TypeLine(Dialogue d, Animator animator, int index)
    {
        d.textComponent.text = string.Empty;
        isTyping = true;
        textSpeed = 0.03f;
        foreach (char c in d.lines[index].ToCharArray())
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PaperBox_Start"))
            {
                d.textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }
        isTyping = false;
        textSpeed = 0.03f;
    }

    IEnumerator WaitForBox(Dialogue d, Animator animator, int index)
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("PaperBox_InProgress"));
        StartCoroutine(TypeLine(d, animator, index));
    }
}
