using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour
{
	private float potionCounter = -1.5f;
	void Start () {
		
	}

	public void OnClickRestartButton(Transform transform)
	{	
		transform.GetComponent<Rigidbody>().AddForce(Vector3.zero);
		transform.GetComponent<Rigidbody>().isKinematic = true;
		transform.localPosition = new Vector3(potionCounter, 0, 0);
		transform.GetComponent<Rigidbody>().isKinematic = false;
		potionCounter += 1.5f;
		
		if (potionCounter > 2)
		{
			potionCounter = -1.5f;
		}
	}

}
