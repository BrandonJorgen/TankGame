using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VictoryCondition : MonoBehaviour
{
    public UnityEvent BossKilled;
    public GameObject Boss;
    
    void Update()
    {
        if (Boss.activeSelf == false)
        {
            BossKilled.Invoke();
        }
    }
}
