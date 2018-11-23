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

    private void Awake()
    {
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
        }
    }
    private void Move(Vector3 touch)
    {
        plantState = 1;
        if (this.gameObject.activeSelf == true)
        {
            transform.position = new Vector3((int)touch.x, 1.0f, (int)touch.z);
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
        if (plantState == 1)
        {
            TouchController.OnHold -= Move;
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
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
