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
    private bool isActive;

    [SerializeField] private Text stats;

    private void Awake()
    {
        plantState = 0;
        /* plant states:
         * 0 - is picked from catalogue
         * 1 - is chosen to be planted
         * 2 - is planted */
    }

    void Start()
    {
        stats.enabled = false;
        moveUp = true;
        isActive = false;

        TouchController.OnHold += Move;
        TouchController.OnTouch += Planting;
    }

    void Update()
    {
        if (plantState == 0)
        {
            Float();
        }
        if(transform.position.y == 0.5f)
        {
            plantState = 2;
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
        }
    }

    private void Float()
    {
        if (transform.position.y > 1.4f)
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
        if (obj == this.gameObject)
        {
            if (plantState == 1 && GameObject.Find("_Logic").GetComponent<GardenController>().grid[(int)transform.position.x - 5, (int)transform.position.z - 5] == 0)
            {
                TouchController.OnHold -= Move;
                transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
                GameObject.Find("_Logic").GetComponent<GardenController>().grid[(int)transform.position.x - 5, (int)transform.position.z - 5] = 1;
            }
            if (plantState == 2) isActive = !isActive;
            if (isActive)
            {
                stats.enabled = true;
                this.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            }
            else
            {
                stats.enabled = false;
                this.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            }
        }
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
