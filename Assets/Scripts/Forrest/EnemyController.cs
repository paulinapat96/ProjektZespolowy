using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{

	[SerializeField] private List<GameObject> enemiesPrefs = new List<GameObject>();
	[SerializeField] private List<GameObject> weedsPrefs = new List<GameObject>();
	private List<GameObject> weedsList =  new List<GameObject>();
	
	private int maxOnceSpawnLimit = 10;
	void Start () {
		Time.timeScale = 1; //przeniesc do gamelogic
		
		StartCoroutine(WaitToSpawnEnemy(2));
		StartCoroutine(WaitToSpawnWeed(25));
	}
	
	public void SpawnEnemy()
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

	public void SpawnEnemy(Vector3 position, int quantity)
	{
		for (int i = 0; i < quantity; i++)
		{
			if (SignRandomize() > 0)
			{
				Instantiate(enemiesPrefs[1], new Vector3(position.x + 3 * SignRandomize(), 0, Random.Range(position.z -3.0f ,position.z + 3.0f)), transform.rotation);
			}
			else
			{
				Instantiate(enemiesPrefs[1], new Vector3(Random.Range(position.x - 3.0f, position.x + 3.0f), 0, position.z + 3 * SignRandomize()), transform.rotation);
			}
		}
	}
	
	private void SpawnWeed()
	{
		Vector3 newPosition = new Vector3(Random.Range(10.0f, 18.0f) * SignRandomize(), 0, Random.Range(10.0f, 12.0f) * SignRandomize());
		Debug.Log(newPosition);
		GameObject weed = Instantiate(weedsPrefs[0], newPosition, transform.rotation);
		weed.GetComponent<Weed>().OnDestroyWeed += DestroyWeed;
		weedsList.Add(weed);

	}

	private void DestroyWeed(GameObject obj)
	{
		for (var i = 0; i < weedsList.Count; i++)
		{
			if (weedsList[i] == obj)
			{
				Weed weed = weedsList[i].GetComponent<Weed>();
				weed.OnDestroyWeed -= DestroyWeed;
				weedsList.RemoveAt(i);
				SpawnEnemy(obj.transform.position, weed.enemyCounter);
				Destroy(obj);
				break;
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
	
	private IEnumerator WaitToSpawnWeed(float waitTime)
	{
		while (true)
		{
			SpawnWeed();
			yield return new WaitForSeconds(waitTime);
			
		}
	}
}
