using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	[SerializeField] private List<GameObject> enemiesPrefs = new List<GameObject>();
	void Start () {
	
	}
	

	void Update () {
		
		
	}

	public void SpawnEnemy()
	{
		Instantiate(enemiesPrefs[0], new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(2.0f, 8.0f), 78), transform.rotation);
	}


}
