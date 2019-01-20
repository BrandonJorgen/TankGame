using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInstantiate : MonoBehaviour
{

	public GameObject Instance;
	public Image CooldownImage;
	private bool isReloading;
	public FloatData ReloadTime;


	void Update()
	{
		if (!isReloading && Input.GetMouseButtonDown(0))
		{
			Instantiate(Instance, transform.position, transform.rotation);
			CooldownImage.fillAmount = 1;
			StartCoroutine(ReloadCycle());
		}

		if (isReloading)
		{
			CooldownImage.fillAmount -= ReloadTime.Value * Time.deltaTime;
		}
	}

	IEnumerator ReloadCycle()
	{
		isReloading = true;
		yield return new WaitForSeconds(ReloadTime.Value);
		isReloading = false;
	}
}