using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerTagCheck : MonoBehaviour
{
	public string TagName;
	public UnityEvent OnTriggerenter, OnTriggerexit;
	
	[Tooltip("The number of times the event is allowed to go off")]
	public float RepeatMax = 1;

	private float repeatCount;

	private void OnTriggerEnter(Collider other)
	{
		if (repeatCount < RepeatMax)
		{
			if (other.gameObject.tag == TagName)
			{
				OnTriggerenter.Invoke();
				repeatCount++;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == TagName)
		{
			OnTriggerexit.Invoke();
		}
	}
}
