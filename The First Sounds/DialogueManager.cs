using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    
    public GameObject talked;
    private Queue<string> sentences;
    public GameObject Approach;
    public Text nameText;
    public Text dialogueText;
    public Animator talk;
    public Animator text;
   
   

    void Start()
    {
        sentences = new Queue<string>();
        
    }
    
    public void StartDialogue(Dialogue dialogue)
    {
        text.SetBool("IsClosed", true);
        talk.SetBool("IsUsed", true);
        FindObjectOfType<PlayerMovement>().Still();

        Approach.gameObject.SetActive(false);

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    void EndDialogue()
    {
        talk.SetBool("IsUsed", false);
        FindObjectOfType<PlayerMovement>().Move();
        Destroy(talked.gameObject);
        
    }
    
}


