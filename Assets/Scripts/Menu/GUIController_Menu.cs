using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIController_Menu : MonoBehaviour
{

	void Start () {
		
	}


    public void OnClickButtonGarden()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickButtonForest()
    {
        SceneManager.LoadScene(2);
    }
}