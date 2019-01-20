using System.Collections;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

	public FloatData LifetimeSeconds;
	
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(LifetimeSeconds.Value);
		Destroy(gameObject);
	}

	private void OnCollisionEnter(Collision other)
	{
		//Destroy(other.gameObject);
		//Destroy(gameObject);
	}
}
