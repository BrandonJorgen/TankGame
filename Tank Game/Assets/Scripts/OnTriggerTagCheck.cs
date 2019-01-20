using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerTagCheck : MonoBehaviour
{
	public string TagName;
	public UnityEvent OnTriggerenter;
	
	// Use this for initialization
	void Start () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == TagName)
		{
			OnTriggerenter.Invoke();
		}
	}
}
