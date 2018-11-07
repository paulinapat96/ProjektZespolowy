using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

<<<<<<< HEAD:Assets/Scripts/GUIController.cs

//using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour
=======
public class GUIController_Menu : MonoBehaviour
>>>>>>> master:Assets/Scripts/Menu/GUIController_Menu.cs
{

	void Start () {
		
	}


    public void OnClickButtonGarden()
    {
        SceneManager.LoadScene(2);
        Debug.Log("Wlasnie wygrales gre!");
    }
}