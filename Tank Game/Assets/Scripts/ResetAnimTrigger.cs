using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimTrigger : MonoBehaviour
{
    private Animator animator;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void ResetTrigger(String triggerName)
    {
        animator.ResetTrigger(triggerName);
    }
}
