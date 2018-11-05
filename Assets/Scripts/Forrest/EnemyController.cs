using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{

	[SerializeField] private List<GameObject> enemiesPrefs = new List<GameObject>();
	private int maxOnceSpawnLimit = 10;
	void Start () {
		
		StartCoroutine(WaitToSpawnEnemy(2));
	}
	

	void Update () {
		
		
	}

	public void SpawnEnemy()
	{
		if (SceneManager.GetActiveScene().name == "Forrest")
		{
			Instantiate(enemiesPrefs[0], new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(2.0f, 8.0f), 78), transform.rotation);
		}

		if (SceneManager.GetActiveScene().name == "Prototype")
		{
			if (SignRandomize() > 0)
			{
				Instantiate(enemiesPrefs[1], new Vector3(20 * SignRandomize(), 0, Random.Range(-30.0f, 30.0f)), transform.rotation);
			}
			else
			{
				Instantiate(enemiesPrefs[1], new Vector3(Random.Range(-20.0f, 20.0f), 0, 30 * SignRandomize()), transform.rotation);
			}
		}
		
	}

	private int SignRandomize()
	{
		int randomSign = Random.Range(1, 10);
		return (randomSign %2 == 0)? -1 : 1;
	}

	private IEnumerator WaitToSpawnEnemy(float waitTime)
	{
		while (true)
		{
			for(int i=0; i< Random.Range(2,maxOnceSpawnLimit); i++) SpawnEnemy();
			yield return new WaitForSeconds(waitTime);
			
		}
	}
}
