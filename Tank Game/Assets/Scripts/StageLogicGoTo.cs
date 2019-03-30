using UnityEngine;
using UnityEngine.Events;

public class StageLogicGoTo : MonoBehaviour
{
    public string Tag = "Player";
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
            if (other.CompareTag(Tag))
            {
                StageCleared = true;
                StageClearedEvent.Invoke();
                eventExecuted = true;
            }
        }
    }
}
