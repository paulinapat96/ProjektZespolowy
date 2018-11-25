using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Plant : MonoBehaviour
{
    private int plantState;
    private bool moveUp;
    [SerializeField] private GameObject tile = new GameObject();

    private void Awake()
    {
        tile = new GameObject();
        tile.SetActive(false);
        tile.GetComponent<Renderer>().material.color = Color.green;

        plantState = 0;
        /* plant states:
         * 0 - is picked from catalogue
         * 1 - is chosen
         * 2 - is planted */
    }
    // Use this for initialization

    void Start()
    {
        TouchController.OnHold += Move;
        TouchController.OnTouch += Planting;

        moveUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (plantState == 0)
        {
            Float();
            tile.SetActive(true);
        }
    }
    private void Move(Vector3 touch)
    {
        plantState = 1;
        if (this.gameObject.activeSelf == true)
        {
            transform.position = new Vector3((int)touch.x, 1.0f, (int)touch.z);
            if (touch.x > 16.0f)
            {
                transform.position = new Vector3((int)(16.0f), 1.0f, (int)touch.z);
            }
            if (touch.z > 16.0f)
            {
                transform.position = new Vector3((int)touch.x, 1.0f, (int)(16.0f));
            }
            if (touch.x > 16.0f && touch.z > 16.0f)
            {
                transform.position = new Vector3((int)(16.0f), 1.0f, (int)(16.0f));
            }
            if (touch.x < 5.0f)
            {
                transform.position = new Vector3((int)(5.0f), 1.0f, (int)touch.z);
            }
            if (touch.z < 5.0f)
            {
                transform.position = new Vector3((int)touch.x, 1.0f, (int)(5.0f));
            }
            if (touch.x < 5.0f && touch.z < 5.0f)
            {
                transform.position = new Vector3((int)(5.0f), 1.0f, (int)(5.0f));
            }
            tile.transform.position = new Vector3(transform.position.x, 0.005f, transform.position.z);
            if (GameObject.Find("_Logic").GetComponent<GardenController>().grid[(int)transform.position.x - 5, (int)transform.position.z - 5] == 1)
                tile.GetComponent<Renderer>().material.color = Color.red;
            else
                tile.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    private void Float()
    {
        if (transform.position.y > 2.0f)
            moveUp = false;
        if (transform.position.y < 1.0f)
            moveUp = true;

        if (moveUp)
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z);
    }

    public void Planting(GameObject obj)
    {
        if (plantState == 1 && GameObject.Find("_Logic").GetComponent<GardenController>().grid[(int)transform.position.x-5,(int)transform.position.z-5] == 0)
        {
            TouchController.OnHold -= Move;
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            GameObject.Find("_Logic").GetComponent<GardenController>().grid[(int)transform.position.x - 5, (int)transform.position.z - 5] = 1;
            tile.SetActive(false);
            tile.transform.position = new Vector3(11.0f, 0.005f, 11.0f);
            plantState = 2;
        }
        if (plantState ==2) Debug.Log("x=" + transform.position.x + "z=" + transform.position.z);
    }

    public int GetPlantState()
    {
        return plantState;
    }

    private void OnDestroy()
    {
        TouchController.OnTouch -= Planting;
        TouchController.OnHold -= Move;
    }

}
