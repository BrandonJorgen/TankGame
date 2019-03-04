using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    [Header("Victory Settings")]
    public GameObject Boss;
    public UnityEvent BossKilled;
    private bool bossSighted =false;
    [Header("Pause Game Settings")] 
    public BoolData Paused;
    public GameObject QuitMenu;
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
        Boss = GameObject.FindWithTag("Boss");

        //Check if a boss even exists in the current Stage
        if (Boss != null)
        {
            bossSighted = true;
        }
        else
        {
            bossSighted = false;
        }
    }

    void Update()
    {
        //VICTORY BEHAVIOR
        if (bossSighted)//If Boss exists
        {
            if (Boss.activeSelf == false)//If boss is dead
            {
                BossKilled.Invoke();
            }
        }
        
        //PAUSE GAME BEHAVIOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale >= 1)
            {
                Time.timeScale = 0; //game set to paused
                Paused.Bool = true;
                OnPaused.Invoke();
            } 
            else if (Time.timeScale <= 0)
            {
                if (!QuitMenu.activeSelf)//The Quit Menu isn't active
                {
                    Time.timeScale = 1; //game set back to playing
                    Paused.Bool = false;
                    OnResume.Invoke();
                }

                if (QuitMenu.activeSelf)//The Quit Menu is active
                {
                    QuitMenu.SetActive(false);
                }
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
