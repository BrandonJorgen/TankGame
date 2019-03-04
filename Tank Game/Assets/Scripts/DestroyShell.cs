using System.Collections;
using UnityEngine;

public class DestroyShell : MonoBehaviour
{
	public FloatData LifetimeSeconds, ShellDamage;
	public GameObject Effect;
	
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(LifetimeSeconds.Value);
		Instantiate(Effect, transform.position, Effect.transform.rotation);
		Destroy(gameObject);
	}

	private void OnCollisionEnter(Collision other)
	{
		if (gameObject.CompareTag("PlayerShell") && other.collider.CompareTag("Player"))
		{
			return;
		}

		if (gameObject.CompareTag("EnemyShell") && other.collider.CompareTag("Enemy"))
		{
			return;
		}
		
		Instantiate(Effect, transform.position, Effect.transform.rotation);
		Destroy(gameObject);
	}
}
