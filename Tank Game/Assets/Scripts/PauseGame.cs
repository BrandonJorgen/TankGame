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
            } else if (UpgradeCount.Value > 0 && ReloadSpeedUpgrade.Bool)
            {
                Time.timeScale = 0;
                OnPaused.Invoke();
                ReloadSpeedPause.Invoke();
            } else if (UpgradeCount.Value > 0 && RangeUpgrade.Bool)
            {
                Time.timeScale = 0;
                OnPaused.Invoke();
                RangePause.Invoke();
            } else if (UpgradeCount.Value > 0 && DamageUpgrade.Bool)
            {
                Time.timeScale = 0;
                OnPaused.Invoke();
                DamagePause.Invoke();
            } else if (UpgradeCount.Value > 0 && MovementSpeedUpgrade.Bool)
            {
                Time.timeScale = 0;
                OnPaused.Invoke();
                MovementSpeedPause.Invoke();
            } else if (UpgradeCount.Value > 0 && ReloadSpeedUpgrade.Bool)
            {
                Time.timeScale = 1;
                OnResume.Invoke();
                ReloadSpeedResume.Invoke();
            } else if (UpgradeCount.Value > 0 && RangeUpgrade.Bool)
            {
                Time.timeScale = 1;
                OnResume.Invoke();
                RangeResume.Invoke();
            } else if (UpgradeCount.Value > 0 && DamageUpgrade.Bool)
            {
                Time.timeScale = 1;
                OnResume.Invoke();
                DamageResume.Invoke();
            } else if (UpgradeCount.Value > 0 && MovementSpeedUpgrade.Bool)
            {
                Time.timeScale = 1;
                OnResume.Invoke();
                MovementSpeedResume.Invoke();
            }
        }
    }
}
