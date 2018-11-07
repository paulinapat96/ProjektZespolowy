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
        SceneManager.LoadScene(2);
        Debug.Log("Wlasnie wygrales gre!");
    }
}
//widziesZ? tak wyglądają zmergowane pliki jak git nie wie co zrobić

    //okej śmiga, to co ja zrbiłąm to usunęąm wszystkie Twoje zmiany tym czerwonym smietniczkiem u góry