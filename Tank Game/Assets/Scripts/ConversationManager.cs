using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    //TODO I may want to change the autoproceed # to be based on the conversation rather than the entire game or change it to an object that gets destroyed rather than the UI itself
    //THIS CLASS HANDLES SHOWING AND MOVING TO THE NEXT LINE OF THE CONVERSATION

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI ConversationText;
    public Image PortraitImage;
    public Sprite UnknownPortrait, GeneralPortrait;
    public Animator Animator;
    public float AutoProceed = 3, ConvoDelay;
    public BoolData ControllerActive;
    public UnityEvent AfterDeactiveAnimEvent, AfterActiveAnimEvent, EndOfConversationEvent;
    private bool animationFinished, cutscene;
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
        PortraitImage.sprite = UnknownPortrait;
        sentences = new Queue<string>();
    }

    public void StartConversation(Conversation dialogue)
    {
        NameText.text = dialogue.Name;
        ConversationText.text = "";
        ConvoDelay = dialogue.Delay;
        if (dialogue.IsCutscene)
        {
            cutscene = true;
        }
        else
        {
            cutscene = false;
        }
        
        Animator.SetBool("NewStage", false);
        sentences.Clear();//Clear all sentences just in case ones from a previous conversation still exist

        foreach (string sentence in dialogue.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        //TODO This code will need to be changed when characters get actual names along with expanding based on the number of NPCs that talk
        //Portrait Selection Code
        if (NameText.text == "General")
        {
            PortraitImage.sprite = GeneralPortrait;
        }
        else
        {
            PortraitImage.sprite = UnknownPortrait;
        }

        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(ConvoDelay);
        yield return null;//Wait a frame just in case this convo starts after level start
        if (cutscene)//Current Loaded conversation is a cutscene
        {
            ControllerActive.Bool = false;//Disable player controls
        }

        if (!cutscene)
        {
            ControllerActive.Bool = true;//Enable player controls
        }
        Animator.SetBool("NewStage", false);
        Animator.SetBool("IsActive", true);//Begin popup animation
    }

    public void NextSentence()//Does the person talking have any lines left? is so, and the animation has finished, show next line, else end convo
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

    void ActiveAnimationComplete()//Runs after the activation popup anim finishes
    {
        animationFinished = true;
        AfterActiveAnimEvent.Invoke();
        NextSentence();
    }
    
    void DeactiveAnimationComplete()//Runs after radio UI element animation finishes
    {
        animationFinished = true;
        
        if (animationFinished)
        {
            AfterDeactiveAnimEvent.Invoke();
            ControllerActive.Bool = true;//General catch-all to make sure the play controller is active, end of cutscene or not
        }
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

    IEnumerator ConvoProceed()//Next line of conversation text is show after this coroutine finishes
    {
        yield return new WaitForSeconds(AutoProceed);
        NextSentence();
    }

    void EndConversation()//More animation code for when the conversation is officially over
    {
        Animator.SetBool("IsActive", false);
        animationFinished = false;
        EndOfConversationEvent.Invoke();
    }
}
