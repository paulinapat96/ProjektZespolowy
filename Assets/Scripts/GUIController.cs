using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour
{
    	private float potionCounter = -1.5f;
	void Start () {
		
	}

    public void OnClickButtonGarden()
    {
        SceneManager.LoadScene(2);
        Debug.Log("Wlasnie wygrales gre!");
    }

	public void OnClickRestartButton(Transform transform)
	{	
		transform.GetComponent<Rigidbody>().AddForce(Vector3.zero);
		transform.GetComponent<Rigidbody>().isKinematic = true;
		transform.localPosition = new Vector3(potionCounter, 0, 0);
		transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
		transform.GetComponent<Rigidbody>().isKinematic = false;
		
		potionCounter += 1.5f;
		
		if (potionCounter > 2)
		{
			potionCounter = -1.5f;
		}
	}

}
