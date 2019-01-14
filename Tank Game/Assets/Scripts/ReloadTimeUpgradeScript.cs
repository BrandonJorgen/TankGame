using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReloadTimeUpgradeScript : MonoBehaviour
{
    public UnityEvent MouseDown;

    // Update is called once per frame
    void OnMouseDown()
    {
        MouseDown.Invoke();
    }
}
