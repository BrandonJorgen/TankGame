using UnityEngine;
using UnityEngine.Events;

public class StageLogicGoTo : MonoBehaviour
{
    public bool StageCleared;
    public UnityEvent StageClearedEvent;
    private bool eventExecuted;

    void Start()
    {
        StageCleared = false;
        eventExecuted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!eventExecuted)
        {
            if (other.CompareTag("Player"))
            {
                StageCleared = true;
                StageClearedEvent.Invoke();
                eventExecuted = true;
            }
        }
    }
}
