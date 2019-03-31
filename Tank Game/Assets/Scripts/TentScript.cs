using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentScript : MonoBehaviour
{
    private Animator animator;
    
    private void Update()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("PlayerInside", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("PlayerInside", false);
    }
}