using UnityEngine;
using UnityEngine.SceneManagement;

public class StageChange : MonoBehaviour
{
    public GameObject NextStageTrigger, Stage;
    public BoolData ControllerActive;
    public Animator animator;
    public int NextSceneToLoad;
    
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
        NextStageTrigger = GameObject.FindWithTag("Portal");
        NextStageTrigger.gameObject.GetComponent<Collider>().isTrigger = true;
        Stage = GameObject.FindWithTag("Stage");
        animator.SetBool("IsChanging", false);
        NextSceneToLoad = NextStageTrigger.GetComponent<PortalScript>().NextLevel;
    }

    private void Update()
    {
        //TODO You may need to expand this with the addition of different level objectives
        if (Stage.name == "StageLogicKillAll")//Kill All Stage logic detected in scene
        {
            if (Stage.GetComponent<StageLogicKillAll>().StageCleared && NextStageTrigger.GetComponent<PortalScript>().isTriggered)
            {
                //All enemies are dead and the player is inside the next stage trigger box
                ControllerActive.Bool = false;//Disable player controls
                animator.SetBool("IsChanging", true);//Fade out
            }
        }

        if (Stage.name == "StageLogicGoTo")//Go To Stage logic detected in scene
        {
            if (Stage.GetComponent<StageLogicGoTo>().StageCleared && NextStageTrigger.GetComponent<PortalScript>().isTriggered)
            {
                //Player has reached destination and is inside the next stage trigger box
                ControllerActive.Bool = false;//Disable player controls
                animator.SetBool("IsChanging", true);//Fade out
            }
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