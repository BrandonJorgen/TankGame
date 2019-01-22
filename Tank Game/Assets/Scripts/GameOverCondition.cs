using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverCondition : MonoBehaviour
{
    public UnityEvent ZeroHealth;
    public Image HealthImage;
    
    void Update()
    {
        if (HealthImage.fillAmount == 0) //Zero Health
        {
            ZeroHealth.Invoke();
        }
    }
}
