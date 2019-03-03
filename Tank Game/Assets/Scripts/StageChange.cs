using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageChange : MonoBehaviour
{
    public GameObject NextStageTrigger, Stage, Player;
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
        Player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        
        //All enemies are dead and the player is inside the next stage trigger box
        if (Stage.GetComponent<StageLogic>().StageCleared && NextStageTrigger.GetComponent<PortalScript>().isTriggered)
        {
            Player.GetComponent<CharacterController>().enabled = false;//Disable player controls
            animator.SetBool("IsChanging", true);//Fade out
        }
    }

    void FadeOutFinished()//Executes at the end of the FadeOut animation using an animation event
    {
        SceneManager.LoadScene(NextSceneToLoad);
    }

    void FadeInFinished()//Executes at the end of the FadeIn animation
    {
        Player.GetComponent<CharacterController>().enabled = true;
    }
}
