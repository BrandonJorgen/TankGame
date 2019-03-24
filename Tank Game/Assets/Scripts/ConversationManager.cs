using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    //THIS CLASS HANDLES SHOWING AND MOVING TO THE NEXT LINE OF THE CONVERSATION

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI ConversationText;
    public Image PortraitImage;
    public Sprite UnknownPortrait, GeneralPortrait;
    public Animator Animator;
    public float AutoProceed = 3, StartConvoDelay;
    public BoolData ControllerActive;
    private bool animationFinished, CutsceneHappening;
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
        if (dialogue.IsCutscene)//Current Loaded conversation is a cutscene
        {
            CutsceneHappening = true;
            ControllerActive.Bool = false;
        }
        else//Current Loaded conversation is not a cutscene
        {
            CutsceneHappening = false;
            ControllerActive.Bool = true;
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
        
        Animator.SetBool("NewStage", false);
        Animator.SetBool("IsActive", true);//Begin popup animation
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

    void ActiveAnimationComplete()//Runs after the activation popup anim finishes
    {
        animationFinished = true;
        NextSentence();
    }
    
    void DeactiveAnimationComplete()
    {
        animationFinished = true;
        
        if (animationFinished)
        {
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
