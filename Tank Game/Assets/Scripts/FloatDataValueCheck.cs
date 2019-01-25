using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatDataValueCheck : MonoBehaviour
{
    public FloatData DataValue;
    public float Value;
    public UnityEvent ValueMatch;
    
    void Update()
    {
        if (Value == DataValue.Value)
        {
            ValueMatch.Invoke();
        }
    }
}
