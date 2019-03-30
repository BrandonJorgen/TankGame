using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public bool isTriggered = false;
    [Tooltip("Level Index of level to be loaded")]
    public int NextLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
        }
        else
        {
            isTriggered = false;
        }
    }
}
