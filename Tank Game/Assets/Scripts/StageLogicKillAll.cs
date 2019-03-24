using UnityEngine;
using UnityEngine.Events;

public class StageLogicKillAll : MonoBehaviour
{
    public GameObject[] Enemies;
    public bool StageCleared = false;
    public UnityEvent StageClearedEvent;
    private bool eventExecuted = false;

    private void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        if (Enemies.Length > 0)
        {
            foreach (GameObject enemy in Enemies)
            {
                if (enemy.activeSelf)//Check if any enemy is still alive
                {
                    StageCleared = false;
                }
                else//Couldn't find one
                {
                    StageCleared = true;
                    if (!eventExecuted)//Set so the event only happens once while in update
                    {
                        StageClearedEvent.Invoke();
                        eventExecuted = true;
                    }
                }
            }
        }
        else
        {
            StageCleared = true;
            if (!eventExecuted)//Set so the event only happens once while in update
            {
                StageClearedEvent.Invoke();
                eventExecuted = true;
            }
        }
    }
}
