using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObectInstantiate : MonoBehaviour
{

	public GameObject Instance;
	public UnityEvent OnClick;


	void OnClickEvent()
	{
		Instantiate(Instance, transform.position, transform.rotation);
	}
}