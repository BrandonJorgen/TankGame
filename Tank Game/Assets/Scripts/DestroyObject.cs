using System.Collections;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
	//TODO *OPTIONAL* ADD ANIMATION FOR WHEN THE BULLET HITS SOMETHING

	public FloatData LifetimeSeconds, ShellDamage;
	
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(LifetimeSeconds.Value);
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
		
		Destroy(gameObject);
	}
}
