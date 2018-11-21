using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenController : MonoBehaviour
{
    // Use this for initialization
    [SerializeField] private List<GameObject> plantPrefs = new List<GameObject>();
    private List<GameObject> plants = new List<GameObject>();

    void Start()
    {
        //TouchController.OnHold += Move;
        GUIController_Garden.OnClick += SpawnPlant;
    }

    public void SpawnPlant(Vector3 spawnPos, int type)
    {
        if (type == 1)
            plants.Add(Instantiate(plantPrefs[0], spawnPos, transform.rotation));
        if (type == 2)
            plants.Add(Instantiate(plantPrefs[1], spawnPos, transform.rotation));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
