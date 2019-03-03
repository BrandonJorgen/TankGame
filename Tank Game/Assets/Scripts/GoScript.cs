using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoScript : MonoBehaviour
{
    private Animator animator;
    
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("GoActivated", true);
    }

    private void OnDisable()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("GoActivated", false);
    }
}
