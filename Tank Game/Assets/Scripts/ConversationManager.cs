using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    //TODO WHAT IF THE PLAYER CHANGES STAGES DURING A CONVERSATION
        //My assumption is that since the gameobject with the trigger assigned to it will be deleted, the convo will just stop
    //TODO IF STATEMENTS BASED ON THE NAME OF THE PERSON TALKING AND TOGGLING CERTAIN BOOLS TO DECIDE WHAT IMAGE TO USE
    //THIS CLASS HANDLES SHOWING AND MOVING TO THE NEXT LINE OF THE CONVERSATION

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI ConversationText;
    public Animator Animator;
    public float AutoProceed = 3;
    private bool animationFinished;
    private Queue<string> sentences;
    
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartConversation(Conversation dialogue)
    {
        Debug.Log("Starting Conversation with: " + dialogue.Name);
        NameText.text = dialogue.Name;
        sentences.Clear();//Clear all sentences just in case ones from a previous conversation still exist

        foreach (string sentence in dialogue.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        Animator.SetBool("IsActive", true);
    }

    public void NextSentence()
    {
        if (sentences.Count == 0)
        {
            EndConversation();
            return;
        }

        string sentence = sentences.Dequeue();
        
        if (animationFinished)
        {
            StartCoroutine(DisplayText(sentence));
        }
    }

    void AnimationComplete()
    {
        animationFinished = true;
        NextSentence();
    }

    IEnumerator DisplayText(string sentence)
    {
        ConversationText.text = "";

        foreach (char letter in sentence.ToCharArray())//Used to scroll out the text rather than showing the entire sentence immediately
        {
            ConversationText.text += letter;
            yield return null;
        }
        Debug.Log(sentence);
        StartCoroutine(ConvoProceed());
    }

    IEnumerator ConvoProceed()
    {
        yield return new WaitForSeconds(AutoProceed);
        NextSentence();
    }

    void EndConversation()
    {
        Animator.SetBool("IsActive", false);
        animationFinished = false;
    }
}
