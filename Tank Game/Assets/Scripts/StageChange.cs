using UnityEngine;
using UnityEngine.SceneManagement;

public class StageChange : MonoBehaviour
{
    public GameObject NextStageTrigger, StageLogic;
    public BoolData ControllerActive;
    public Animator animator;
    public int NextSceneToLoad;
    private bool isGoTo, isKillAll;
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneChangeManagement;
        animator = GetComponent<Animator>();
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneChangeManagement;
    }

    private void SceneChangeManagement(Scene scene, LoadSceneMode current)
    {
        NextStageTrigger = GameObject.FindWithTag("Portal");//Finds Next Level Trigger in current scene
        if (NextStageTrigger != null)
        {
            NextStageTrigger.gameObject.GetComponent<Collider>().isTrigger = true;//Makes sure the Trigger is actually a trigger
        }
        else
        {
            return;
        }
        
        StageLogic = GameObject.FindWithTag("Stage");//Finds Stage Logic in current scene
        
        if (StageLogic == null)
        {
            Debug.Log("No Stage Logic is present, Please add one to the current scene");
        }

        //Add to this list to add additional Stage Logic Objectives
        if (StageLogic.GetComponent<StageLogicGoTo>() != null)
        {
            isGoTo = true;
        }
        
        if (StageLogic.GetComponent<StageLogicKillAll>() != null)
        {
            isKillAll = true;
        }
        
        animator.SetBool("IsChanging", false);
        NextSceneToLoad = NextStageTrigger.GetComponent<PortalScript>().NextLevel;
    }

    private void Update()
    {
        //Add to this list to add additional Stage Logic Objectives
        if (isGoTo && NextStageTrigger.GetComponent<PortalScript>().isTriggered)
        {
            //All enemies are dead and the player is inside the next stage trigger box
            ControllerActive.Bool = false;//Disable player controls
            animator.SetBool("IsChanging", true);//Fade out
        }
        
        if (isKillAll && NextStageTrigger.GetComponent<PortalScript>().isTriggered)
        {
            //All enemies are dead and the player is inside the next stage trigger box
            ControllerActive.Bool = false;//Disable player controls
            animator.SetBool("IsChanging", true);//Fade out
        }
    }

    void FadeOutFinished()//Executes at the end of the FadeOut animation using an animation event
    {
        
        SceneManager.LoadScene(NextSceneToLoad);
    }

    void FadeInFinished()//Executes at the end of the FadeIn animation
    {
        ControllerActive.Bool = true;//Enable player controls
    }
}