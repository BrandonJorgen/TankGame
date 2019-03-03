using System.Collections;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

	public FloatData LifetimeSeconds, ShellDamage;
	
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(LifetimeSeconds.Value);
		Destroy(gameObject);
	}
}
