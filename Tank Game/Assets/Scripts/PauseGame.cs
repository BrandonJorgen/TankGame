using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseGame : MonoBehaviour
{
    public UnityEvent OnPaused, OnResume, ReloadSpeedPause, ReloadSpeedResume, RangePause;
    public UnityEvent RangeResume, DamagePause, DamageResume, MovementSpeedPause, MovementSpeedResume;
    public FloatData UpgradeCount;
    public BoolData ReloadSpeedUpgrade, RangeUpgrade, DamageUpgrade, MovementSpeedUpgrade;
    
    void Update()
    {
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
    }
}
