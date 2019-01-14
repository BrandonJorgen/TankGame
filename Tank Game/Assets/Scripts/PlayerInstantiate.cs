using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstantiate : MonoBehaviour
{

	public GameObject Instance;
	private bool isReloading;
	public FloatData ReloadTime;


	void Update()
	{
		if (!isReloading && Input.GetMouseButtonDown(0))
		{
			Instantiate(Instance, transform.position, transform.rotation);
			StartCoroutine(ReloadCycle());
		}
	}

	IEnumerator ReloadCycle()
	{
		isReloading = true;
		yield return new WaitForSeconds(ReloadTime.Value);
		isReloading = false;
	}
}