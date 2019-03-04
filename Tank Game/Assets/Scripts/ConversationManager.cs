using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ConversationManager : MonoBehaviour
{
    //THIS CLASS HANDLES SHOWING AND MOVING TO THE NEXT LINE OF THE CONVERSATION

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI ConversationText;
    public Animator Animator;
    public float AutoProceed = 3;
    private bool animationFinished;
    private Queue<string> sentences;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneChangeManagement;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneChangeManagement;
    }

    private void SceneChangeManagement(Scene scene, LoadSceneMode current)
    {
        Animator.SetBool("NewStage", true);
        Animator.SetBool("IsActive", false);
    }
    
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartConversation(Conversation dialogue)
    {
        NameText.text = dialogue.Name;
        ConversationText.text = "";
        Animator.SetBool("NewStage", false);
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
