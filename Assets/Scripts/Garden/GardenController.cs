using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenController : MonoBehaviour {

    // Use this for initialization
    /*[SerializeField] private List<GameObject> plantPrefs = new List<GameObject>();
    GameObject plant;
    private Camera cam;
    private Vector2 fingerPos;
    private Vector3 plantPos;*/

    void Start () {
        //cam = Camera.main;
        TouchController.OnHold += Moving;
    }

    private void Moving(Vector3 touch)
    {
        Debug.Log("x=" + touch.x + " y=" + touch.y + " z=" + touch.z);
        transform.position = new Vector3(touch.x, 0, touch.z);
    }

    private void OnDestroy()
    {
        TouchController.OnHold -= Moving;
    }

    // Update is called once per frame
    void Update () {

        
        //Debug.Log("x=" + transform.position.x + "y=" + transform.position.y + "z=" + transform.position.z);

        /* if (Input.touchCount > 0)
         {
             Touch touch = Input.GetTouch(0);

             switch (touch.phase)
             {

                 case TouchPhase.Began:
                     fingerPos = Input.GetTouch(0).position;
                     //Debug.Log("x=" + fingerPos.x + "y=" + fingerPos.y);
                     plantPos = cam.ScreenToWorldPoint(new Vector3(fingerPos.x, fingerPos.y, 10.0f));
                     SpawnPlant(plantPos.x,plantPos.y,plantPos.z);
                     break;


                 case TouchPhase.Moved:
                     fingerPos = Input.GetTouch(0).position;
                     plant.transform.position = cam.ScreenToWorldPoint(new Vector3(fingerPos.x, fingerPos.y, 10.0f));
                     //Debug.Log("x=" + plantPos.x + "y=" + plantPos.y);
                     //Debug.Log("x=" + plantPos.x + "y=" + plantPos.y + "z=" + plantPos.z);
                     break;

                 case TouchPhase.Ended:
                     DeletePlant();
                     break;
             }
         } */
    }

    /*public void SpawnPlant(float x,float y,float z)
    {
        plant=Instantiate(plantPrefs[0], new Vector3(x,y,z), transform.rotation);
    }

    public void DeletePlant()
    {
        Destroy(plant);
    }*/
}
