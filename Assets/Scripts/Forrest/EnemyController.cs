using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{

	[Serializable]
	public struct WinCondition
	{
		[SerializeField] private Text _enemiesLeft;
		
		public KillPair[] _enemiesToKill;
		private KillPair[] _possibleEnemiesToKill;
		
		private Action _onWon;

		public void Init()
		{
			_possibleEnemiesToKill = _enemiesToKill.ToArray();
		}

		public void AssignOnWon(Action onWon)
		{
			_onWon = onWon;
			
			UpdateEnemiesLeft();
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

			UpdateEnemiesLeft();

			if (CheckWinCondition())
			{
				if (_onWon != null) _onWon();
			}
		}

		public string GetNextEnemy()
		{
			if (_possibleEnemiesToKill.Length == 0) return ""; 
			
			int randomEnemy = Random.Range(0, _possibleEnemiesToKill.Length);

			_possibleEnemiesToKill[randomEnemy].Amount--;

			string enemyToReturn = _possibleEnemiesToKill[randomEnemy].Type;

			if (_possibleEnemiesToKill[randomEnemy].Amount <= 0)
				_possibleEnemiesToKill = _possibleEnemiesToKill.Where(arg => arg.Amount > 0).ToArray();

//			Debug.Log("Returning new enemy: " + enemyToReturn + " left: ");
//			foreach (var killPair in _possibleEnemiesToKill)
//			{
//				Debug.LogFormat("{0}: {1}", killPair.Type, killPair.Amount);
//			}
			return enemyToReturn;
		}

		public bool CheckWinCondition()
		{
			return _enemiesToKill.All(arg => arg.Amount <= 0);
		}
		
		

		private void UpdateEnemiesLeft()
		{
			_enemiesLeft.text = _enemiesToKill.Sum(arg => arg.Amount).ToString();
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

	[SerializeField] private GameObject _result;
	
	private int maxOnceEnemySpawnLimit = 10;
	private float timeToSpawnEnemy = 3;
	private float timeToSpawnWeed = 25;

	private WinCondition _myWinCondition;
	
	private const int TIME_TO_SPAWN_WEED_ENEMIES = 15;



	private event Action _playerWon;

	private GameObject NameToPrefab(string name)
	{
		if (name.Equals("Worm")) return enemiesPrefs[1];
		if (name.Equals("LadyBug")) return enemiesPrefs[0];

		return null;
	}
	
	
	void Start ()
	{

		_myWinCondition = _WinCondition;
		_myWinCondition.Init();
		_myWinCondition.AssignOnWon(PlayerWon);
		
		Time.timeScale = 1; //przeniesc do gamelogic
		
		StartCoroutine(WaitToSpawnEnemy(timeToSpawnEnemy));
		StartCoroutine(WaitToSpawnWeed(timeToSpawnWeed));
	}


	
	public void SpawnEnemy()
	{
		string enemyType = _myWinCondition.GetNextEnemy();

		if (string.IsNullOrEmpty(enemyType)) return;

		var prefab = NameToPrefab(enemyType);
		
		int sign = SignRandomize();
		Vector3 position = sign > 0
			? new Vector3(20 * SignRandomize(), 0, Random.Range(-30.0f, 30.0f))
			: new Vector3(Random.Range(-20.0f, 20.0f), 0, 30 * SignRandomize());
		
		GameObject go = Instantiate(prefab, position, transform.rotation);
		Enemy enemy = go.GetComponent<Enemy>();  // TODO: Spawn enemy instead of gameobject
		enemy.OnDie += _myWinCondition.EnemyDestroyed;

	}

	
	public void SpawnEnemy(Vector3 position, int quantity)
	{
		for (int i = 0; i < quantity; i++)
		{
			string enemyType = _myWinCondition.GetNextEnemy();

			if (string.IsNullOrEmpty(enemyType)) return;

			var prefab = NameToPrefab(enemyType);

			
			if (SignRandomize() > 0)
			{
				GameObject go =Instantiate(prefab, new Vector3(position.x + 3 * SignRandomize(), 0, Random.Range(position.z -3.0f ,position.z + 3.0f)), transform.rotation);
				Enemy enemy = go.GetComponent<Enemy>();  // TODO: Spawn enemy instead of gameobject
				enemy.OnDie += _myWinCondition.EnemyDestroyed;
			}
			else
			{
				GameObject go =Instantiate(prefab, new Vector3(Random.Range(position.x - 3.0f, position.x + 3.0f), 0, position.z + 3 * SignRandomize()), transform.rotation);
				Enemy enemy = go.GetComponent<Enemy>();  // TODO: Spawn enemy instead of gameobject
				enemy.OnDie += _myWinCondition.EnemyDestroyed;
			}
		}
	}
	
	private void PlayerWon()
	{
		Debug.Log("Won won won!");

		Time.timeScale = 0.01f;

		_result.gameObject.SetActive(true);
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

				StartCoroutine(SpawnByTime(obj.transform.position, weed.enemyCounter));
							
				Destroy(obj);
				break;
			}
		}
	}

	private IEnumerator SpawnByTime(Vector3 position, int amount)
	{
		int enemiesToSpawn = amount;
		float spawnRate = TIME_TO_SPAWN_WEED_ENEMIES / (float) amount ;
		while (enemiesToSpawn > 0)
		{
			SpawnEnemy(position, 1);
			--enemiesToSpawn;
			
			yield return new WaitForSeconds(spawnRate);
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
