using System.Collections;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

	public float LifetimeSeconds = 2;
	
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(LifetimeSeconds);
		Destroy(gameObject);
	}

	private void OnCollisionEnter(Collision other)
	{
		//Destroy(other.gameObject);
		//Destroy(gameObject);
	}
}
