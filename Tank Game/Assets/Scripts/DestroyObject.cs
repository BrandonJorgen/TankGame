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

	private void OnCollisionEnter(Collision other)
	{
		Debug.Log("Collision Detected");
		if (other.collider.CompareTag("Enemy"))
		{
			Debug.Log("Collided with Enemy");
			//Enemy "takes damage" by adding the shelldamage value to thie enemy's hit value
			GetComponent<FloatDataValueCheck>().Value += ShellDamage.Value;
		}
		//Destroy(other.gameObject);
		//Destroy(gameObject);
	}
}
