using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{

	[Serializable]
	public struct WinCondition
	{
		public KillPair[] _enemiesToKill;
		private Action _onWon;

		public void AssignOnWon(Action onWon)
		{
			_onWon = onWon;
		}

		public void EnemyDestroyed(string type)
		{
			for (var i = 0; i < _enemiesToKill.Length; i++)
			{
				if (_enemiesToKill[i].Type == type)
				{
					_enemiesToKill[i].Amount--;

					break;
				}
			}

			if (CheckWinCondition())
			{
				if (_onWon != null) _onWon();
			}
		}

		public bool CheckWinCondition()
		{
			return _enemiesToKill.All(arg => arg.Amount <= 0);
		}
	}

	[Serializable]
	public struct KillPair
	{
		public string Type;
		public int Amount;
	}

	[Header("Win condition")] 
	[SerializeField] private WinCondition _WinCondition;
	
	[Header("Enemies")]
	[SerializeField] private List<GameObject> enemiesPrefs = new List<GameObject>();
	[SerializeField] private List<GameObject> weedsPrefs = new List<GameObject>();
	private List<GameObject> weedsList =  new List<GameObject>();
	
	private int maxOnceEnemySpawnLimit = 10;
	private float timeToSpawnEnemy = 3;
	private float timeToSpawnWeed = 25;

	private WinCondition _myWinCondition;
	
	private const int INTERVAL_TO_SPAWN_ENEMIES = 3;
	private const int WEED_SPAWNED_ENEMIES_IN_ONE_SHOT = 10;

	private event Action _playerWon;

	
	void Start ()
	{

		_myWinCondition = _WinCondition;
		_myWinCondition.AssignOnWon(PlayerWon);
		
		Time.timeScale = 1; //przeniesc do gamelogic
		
		StartCoroutine(WaitToSpawnEnemy(timeToSpawnEnemy));
		StartCoroutine(WaitToSpawnWeed(timeToSpawnWeed));
	}


	
	public void SpawnEnemy()
	{
		int sign = SignRandomize();
		Vector3 position = sign > 0
			? new Vector3(20 * SignRandomize(), 0, Random.Range(-30.0f, 30.0f))
			: new Vector3(Random.Range(-20.0f, 20.0f), 0, 30 * SignRandomize());
		
		GameObject go = Instantiate(enemiesPrefs[1], position, transform.rotation);
		Enemy enemy = go.GetComponent<Enemy>();  // TODO: Spawn enemy instead of gameobject
		enemy.OnDie += _myWinCondition.EnemyDestroyed;

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
	
	private void PlayerWon()
	{
		Debug.Log("Won won won!");
	}
	
	private void SpawnWeed()
	{
		Vector3 newPosition;
		
		if (SignRandomize() > 0)
		{
			newPosition = new Vector3(Random.Range(-12.0f, 12.0f), 0, Random.Range(11.0f, 17.0f) * SignRandomize());
		}
		else
		{
			newPosition = new Vector3(Random.Range(11.0f, 12.0f) * SignRandomize(), 0,Random.Range(-17.0f, 17.0f));
		}
		
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

				StartCoroutine(SpawnByTime(obj.transform.position, weed.enemyCounter, WEED_SPAWNED_ENEMIES_IN_ONE_SHOT));
							
				Destroy(obj);
				break;
			}
		}
	}

	private IEnumerator SpawnByTime(Vector3 position, int amount, int spawnBy)
	{
		int enemiesToSpawn = amount;

		while (enemiesToSpawn > 0)
		{
			if (enemiesToSpawn > spawnBy)
			{
				SpawnEnemy(position, spawnBy);
				enemiesToSpawn -= spawnBy;
			}
			else
			{
				SpawnEnemy(position, enemiesToSpawn);
				enemiesToSpawn = 0;
			}
			yield return new WaitForSeconds(INTERVAL_TO_SPAWN_ENEMIES);
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
			for(int i=0; i< Random.Range(2,maxOnceEnemySpawnLimit); i++) SpawnEnemy();
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
