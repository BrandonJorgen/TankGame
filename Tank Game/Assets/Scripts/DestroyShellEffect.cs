using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class DestroyShellEffect : MonoBehaviour
{
    private Animator animator;
    
    private void OnEnable()
    {
        //Start Animation then call its own destruction
        animator = GetComponent<Animator>();
        animator.SetBool("IsAnimating", true);
    }

    public void DestroyObject()//Destroys object at the end of animation
    {
        Destroy(gameObject);
    }
}
