using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class ForrestController : MonoBehaviour {

	[SerializeField] private GameObject potionsHolder;
	List<GameObject> potionsList = new List<GameObject>();
	
	
	void Start () {
		potionsList = Resources.LoadAll<GameObject>("Potions").ToList();
		Potion.OnDestroy += IsDestroyedPotion;
	}
	
	
	void Update () {
		
	}
	
	private void IsDestroyedPotion(GameObject obj)
	{
		if (obj.name == "BluePotion")
		{
			SpawnPotion(0);
		}
		
		if (obj.name == "GreenPotion")
		{
			SpawnPotion(1);
		}
		
		if (obj.name == "RedPotion")
		{
			SpawnPotion(2);
		}


	}

	void SpawnPotion(int type)
	{
		GameObject obj = Instantiate(potionsList[type], transform.position, transform.rotation);
		obj.transform.SetParent(potionsHolder.transform);
		
		switch (type)
		{
			case 0:
				obj.name = "BluePotion";
				obj.transform.localPosition = new Vector3(-1.5f, 0, 0);
				break;
			
			case 1:
				obj.name = "GreenPotion";
				obj.transform.localPosition = new Vector3(0, 0, 0);
				break;
			
			case 2:
				obj.name = "RedPotion";
				obj.transform.localPosition = new Vector3(1.5f, 0, 0);
				break;
				
		}

	}
}
