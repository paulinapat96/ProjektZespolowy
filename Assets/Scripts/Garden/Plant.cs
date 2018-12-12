using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Plant : MonoBehaviour
{
    private plantStates plantState;
    private bool isActive;
    private int gridX, gridY;

    [SerializeField] private Text plantStats;

    private void Awake()
    {
        plantState = plantStates.IS_PICKED;
    }

    void Start()
    {
        gridX = (int)transform.position.x + 5;
        gridY = (int)transform.position.z + 5;
        plantStats.enabled = false;
        isActive = false;

        TouchController.OnHold += Move;
        TouchController.OnTouch += OnTouch;
    }

    void Update()
    {
        gridX = (int)transform.position.x + 5;
        gridY = (int)transform.position.z + 5;
        if (transform.position.y == 0.5f)
        {
            plantState = plantStates.IS_PLANTED;
        }
    }
    private void Move(Vector3 touch)
    {
        plantState = plantStates.IS_BEING_PLANTED;
        if (this.gameObject.activeSelf == true)
        {
            transform.position = new Vector3((int)touch.x, 1.0f, (int)touch.z);
            BoundariesCollision(touch);
        }
    }

    private void BoundariesCollision(Vector3 touch)
    {
        if (Mathf.Abs(touch.x) > 5.0f)
        {
            transform.position = new Vector3((int)(Mathf.Sign(touch.x)*5.0f), 1.0f, transform.position.z);
        }
        if (Mathf.Abs(touch.z) > 5.0f)
        {
            transform.position = new Vector3(transform.position.x, 1.0f, (int)(Mathf.Sign(touch.z)*5.0f));
        }
    }

    public void OnTouch(GameObject obj)
    {
        if (obj == this.gameObject)
        {
            if (plantState == plantStates.IS_BEING_PLANTED && !GardenController.grid[gridX, gridY])
            {
                TouchController.OnHold -= Move;
                transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
                GardenController.grid[gridX, gridY] = true;
            }
            if (plantState == plantStates.IS_PLANTED) isActive = !isActive;
            if (isActive)
            {
                plantStats.enabled = true;
                this.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            }
            else
            {
                plantStats.enabled = false;
                this.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            }
        }
    }

    public plantStates GetPlantState()
    {
        return plantState;
    }

    private void OnDestroy()
    {
        TouchController.OnTouch -= OnTouch;
        TouchController.OnHold -= Move;
    }
}
