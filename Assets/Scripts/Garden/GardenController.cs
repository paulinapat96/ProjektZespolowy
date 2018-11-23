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
    private int[,] grid = new int[12, 12];

    void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                grid[i, j] = 0;
            }
        }
        plantPrefs[0].SetActive(false);
        plantPrefs[1].SetActive(false);
        GUIController_Garden.OnClick += SpawnPlant;
    }

    public void SpawnPlant(Vector3 spawnPos, int type)
    {
        if (type == 0)
        {
            if (plants.Count == 0 || plants[plants.Count - 1].GetComponent<Plant>().GetPlantState() == 2)
            {
                plants.Add(Instantiate(plantPrefs[0], plantPrefs[0].transform.position, transform.rotation));
                plants[plants.Count - 1].SetActive(true);
            }
            else if (plants[plants.Count - 1].GetComponent<Plant>().GetPlantState() != 2)
            {
                Destroy(plants[plants.Count - 1]);
                plants.RemoveAt(plants.Count - 1);
                plants.Add(Instantiate(plantPrefs[0], plantPrefs[0].transform.position, transform.rotation));
                plants[plants.Count - 1].SetActive(true);
            }
        }
        if (type == 1)
        {
            if (plants.Count == 0 || plants[plants.Count - 1].GetComponent<Plant>().GetPlantState() == 2)
            {
                plants.Add(Instantiate(plantPrefs[1], plantPrefs[1].transform.position, transform.rotation));
                plants[plants.Count - 1].SetActive(true);
            }
            else if (plants[plants.Count - 1].GetComponent<Plant>().GetPlantState() != 2)
            {
                Destroy(plants[plants.Count - 1]);
                plants.RemoveAt(plants.Count - 1);
                plants.Add(Instantiate(plantPrefs[1], plantPrefs[1].transform.position, transform.rotation));
                plants[plants.Count - 1].SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        GUIController_Garden.OnClick -= SpawnPlant;
    }


    void Update()
    {

    }
}
