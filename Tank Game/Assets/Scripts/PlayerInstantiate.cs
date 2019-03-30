using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInstantiate : MonoBehaviour
{
	public GameObject Instance;
	public Image CooldownImage;
	public BoolData GamePaused;
	private bool isReloading;
	public FloatData ReloadTime;

	private void Start()
	{
		CooldownImage = GameObject.FindWithTag("Cooldown").GetComponent<Image>();
	}

	void Update()
	{
		if (!GamePaused.Bool)
		{
			if (!isReloading && Input.GetMouseButtonDown(0))
			{
				Instantiate(Instance, transform.position, transform.rotation);
				CooldownImage.fillAmount = 1;
				StartCoroutine(ReloadCycle());
			}

			if (isReloading)
			{
				CooldownImage.fillAmount -= (1 / ReloadTime.Value) * Time.deltaTime;
			}
		}
	}

	IEnumerator ReloadCycle()
	{
		isReloading = true;
		yield return new WaitForSeconds(ReloadTime.Value);
		isReloading = false;
	}
}