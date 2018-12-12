using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Plant : MonoBehaviour
{
    private enum lifeStates
    {
        IS_DYING,
        IS_GROWING,
        IS_READY_TO_HARVEST
    }
    private GardenController.plantStates plantState;
    private lifeStates lifeState;
    private int waterCounter;
    [SerializeField] private int waterDemand;


    [SerializeField] private int value;

    private int gridX, gridY;

    [SerializeField] private Image timeBar;
    private int timeToHarvest;
    [SerializeField] private int timeToGrow;


    [SerializeField] private Image plantEmotion;
    [SerializeField] private Sprite sadFace; 
    [SerializeField] private Sprite happyFace; 

    public event Action<GameObject> OnDestroyPlant;

    private void Awake()
    {
        plantState = GardenController.plantStates.IS_PICKED;
    }

    void Start()
    {
        gridX = (int)transform.position.x + 5;
        gridY = (int)transform.position.z + 5;
        plantEmotion.enabled = false;
        timeBar.enabled = false;

        lifeState = lifeStates.IS_DYING;
        waterCounter = 0;
        timeToHarvest = 0;

        TouchController.OnHold += Move;
        TouchController.OnTouch += OnTouch;
    }

    void Update()
    {
        Time.timeScale = 1;
        if (plantState != GardenController.plantStates.IS_PLANTED)
        {
            gridX = (int)transform.position.x + 5;
            gridY = (int)transform.position.z + 5;
        }
        if (transform.position.y == 0.5f)
        {
            plantState = GardenController.plantStates.IS_PLANTED;
        }
    }
    private void Move(Vector3 touch)
    {
        plantState = GardenController.plantStates.IS_BEING_PLANTED;
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
            if (plantState == GardenController.plantStates.IS_BEING_PLANTED && !GardenController.grid[gridX, gridY])
            {
                Planting();
            }
            if (plantState == GardenController.plantStates.IS_PLANTED)
            {
                if (lifeState == lifeStates.IS_DYING)
                {    
                    Watering();         
                }
                if (lifeState == lifeStates.IS_READY_TO_HARVEST)
                {
                    Harvest();
                }
            }
        }
    }

    private void Planting()
    {
        TouchController.OnHold -= Move;
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        GardenController.grid[gridX, gridY] = true;
        plantEmotion.enabled = true;
        plantEmotion.sprite = sadFace;
    }
    private void Watering()
    {
        waterCounter++;
        if (waterCounter >= waterDemand)
        {
            lifeState = lifeStates.IS_GROWING;
            StartCoroutine("IncreaseTime");
            plantEmotion.enabled = false;
            timeBar.enabled = true;
        }
    }

    void Harvest()
    {
        GardenController.grid[gridX, gridY] = false;
        GardenController.goldCount += value;
        TouchController.OnTouch -= OnTouch;
        TouchController.OnHold -= Move;
        if (OnDestroyPlant != null) OnDestroyPlant(gameObject);
    }

    public GardenController.plantStates GetPlantState()
    {
        return plantState;
    }

    IEnumerator IncreaseTime()
    {
        while (true)
        {
            timeToHarvest++;
            timeBar.fillAmount = (float)timeToHarvest / timeToGrow;
            if (timeToHarvest == timeToGrow+1)
            {
                lifeState = lifeStates.IS_READY_TO_HARVEST;
                plantEmotion.enabled = true;
                timeBar.enabled = false;
                plantEmotion.sprite = happyFace;
                StopCoroutine("IncreaseTime");
            }
            yield return new WaitForSeconds(1.0f);
            Debug.Log(timeToHarvest);
        }
    }

    private void OnDestroy()
    {
        TouchController.OnTouch -= OnTouch;
        TouchController.OnHold -= Move;
    }
}
