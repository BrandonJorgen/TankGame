using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventBehavior : MonoBehaviour
{

	public UnityEvent Enable;
	public UnityEvent TriggerEnter;
	public UnityEvent TriggerExit;
	
	private void OnEnable()
	{
		Enable.Invoke();
	}

	private void OnTriggerEnter()
	{
		TriggerEnter.Invoke();
	}

	private void OnTriggerExit()
	{
		TriggerExit.Invoke();
	}
}
