using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    //TODO WE MAY RUN INTO ISSUES IF WE IMPLEMENT ANY OTHER MENU LIKE AN OPTIONS MENU WHILE PAUSED, SPECIFICALLY WHEN WE UNPAUSE WHILE IN THE OPTIONS MENU
    //TODO NEED A WAY FOR THE GAME TO NOT CARE IF YOU WON UNLESS YOU HAD AN ACTUAL CHANCE TO FIGHT THE BOSS
    
    [Header("Victory Settings")]
    public GameObject Boss;
    public UnityEvent BossKilled;
    [Header("Pause Game Settings")] 
    public UnityEvent OnPaused;
    public UnityEvent OnResume;
    [Header("Game Over Settings")]
    public FloatData PlayerHealthData;
    public UnityEvent ZeroHealth;
    private GameObject player;

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
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        //VICTORY BEHAVIOR
        if (Boss.activeSelf == false)
        {
            BossKilled.Invoke();
        }
        
        //PAUSE GAME BEHAVIOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0; //game set to paused
                OnPaused.Invoke();
            } else if (Time.timeScale == 0)
            {
                Time.timeScale = 1; //game set back to playing
                OnResume.Invoke();
            }
        }
        
        //GAME OVER BEHAVIOR
        // Check if Player Health is at least 0
        if (PlayerHealthData.Value <= 0) //Zero Health
        {
            ZeroHealth.Invoke();
            player.GetComponent<CharacterController>().enabled = false;
        }
    }
}
