using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEngine.UI;

public class GardenControler : MonoBehaviour {

    // Use this for initialization
    [SerializeField] private List<GameObject> plantPrefs = new List<GameObject>();
    GameObject plant;
    private Camera cam;
    private Vector2 fingerPos;
    private Vector3 plantPos;

    void Start () {
        cam = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
               
                case TouchPhase.Began:
                    fingerPos = Input.GetTouch(0).position;
                    //Debug.Log("x=" + fingerPos.x + "y=" + fingerPos.y);
                    plantPos = cam.ScreenToWorldPoint(new Vector3(fingerPos.x, fingerPos.y, 10.0f));
                    //Debug.Log("x=" + plantPos.x + "y=" + plantPos.y);
                    SpawnPlant(plantPos.x,plantPos.y,plantPos.z);
                    break;

               
                case TouchPhase.Moved:
                    fingerPos = Input.GetTouch(0).position;
                    plant.transform.position = cam.ScreenToWorldPoint(new Vector3(fingerPos.x, fingerPos.y, 10.0f));
                    break;

                case TouchPhase.Ended:
                    DeletePlant();
                    break;
            }
        }
    }

    public void SpawnPlant(float x,float y,float z)
    {
        plant=Instantiate(plantPrefs[0], new Vector3(x,y,z), transform.rotation);
    }

    public void DeletePlant()
    {
        Destroy(plant);
    }
}
