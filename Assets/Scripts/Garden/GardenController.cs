using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenController : MonoBehaviour
{
    [SerializeField] private List<GameObject> plantPrefs = new List<GameObject>();
    private List<GameObject> plants = new List<GameObject>();
    public int[,] grid = new int[12, 12];

    [SerializeField] private List<GameObject> tile = new List<GameObject>();

    void Start()
    {
        tile[0].SetActive(false);
        tile[0].GetComponent<Renderer>().material.color = Color.green;

        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                grid[i, j] = 0;
            }
        }
        plantPrefs[0].SetActive(false);
        plantPrefs[1].SetActive(false);
        plantPrefs[0].GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        plantPrefs[1].GetComponent<Renderer>().material.DisableKeyword("_EMISSION");

        GUIController_Garden.OnClick += SpawnPlant;
    }

    void Update()
    {
        if (plants.Count > 0)
        {
            if (plants[plants.Count - 1].GetComponent<Plant>().GetPlantState() == 2)
            {
                tile[0].SetActive(false);
            }
            else
            {
                tile[0].transform.position = new Vector3(plants[plants.Count - 1].transform.position.x, 0.005f, plants[plants.Count - 1].transform.position.z);
                if (grid[(int)plants[plants.Count - 1].transform.position.x - 5, (int)plants[plants.Count - 1].transform.position.z - 5] == 1)
                    tile[0].GetComponent<Renderer>().material.color = Color.red;
                else
                    tile[0].GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }

    public void SpawnPlant(Vector3 spawnPos, int type)
    {
        tile[0].SetActive(true);
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
}
