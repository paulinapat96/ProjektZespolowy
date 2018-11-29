using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum plantStates
{
    IS_PICKED,
    IS_BEING_PLANTED,
    IS_PLANTED,
}

public class GardenController : MonoBehaviour
{
    [SerializeField] private List<GameObject> plantPrefs = new List<GameObject>();
    private List<GameObject> plants = new List<GameObject>();
    [SerializeField] private List<GameObject> tile = new List<GameObject>();
    public static bool[,] grid = new bool[11, 11];
    plantStates newPlantState;

    void Start()
    {
        newPlantState = 0;
        tile[0].GetComponent<Renderer>().material.color = Color.green;

        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                grid[i, j] = false;
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
            newPlantState = plants[plants.Count - 1].GetComponent<Plant>().GetPlantState();
            if (newPlantState == plantStates.IS_PLANTED)
            {
                tile[0].SetActive(false);
            }
            else
            {
                tile[0].transform.position = new Vector3(plants[plants.Count - 1].transform.position.x, 0.005f, plants[plants.Count - 1].transform.position.z);
                TileHandler((int)plants[plants.Count - 1].transform.position.x + 5, (int)plants[plants.Count - 1].transform.position.z + 5);
            }
        }
    }

    private void TileHandler(int x, int y)
    {
        if (grid[x, y])
            tile[0].GetComponent<Renderer>().material.color = Color.red;
        else
            tile[0].GetComponent<Renderer>().material.color = Color.green;
    }

    public void SpawnPlant(int type)
    {
        tile[0].SetActive(true);
        if (type == 0)
        {
            if (plants.Count == 0 || newPlantState == plantStates.IS_PLANTED)
            {
                plants.Add(Instantiate(plantPrefs[0], plantPrefs[0].transform.position, transform.rotation));
                plants[plants.Count - 1].SetActive(true);
            }
            else if (newPlantState != plantStates.IS_PLANTED)
            {
                Destroy(plants[plants.Count - 1]);
                plants.RemoveAt(plants.Count - 1);
                plants.Add(Instantiate(plantPrefs[0], plantPrefs[0].transform.position, transform.rotation));
                plants[plants.Count - 1].SetActive(true);
            }
        }
        if (type == 1)
        {
            if (plants.Count == 0 || newPlantState == plantStates.IS_PLANTED)
            {
                plants.Add(Instantiate(plantPrefs[1], plantPrefs[1].transform.position, transform.rotation));
                plants[plants.Count - 1].SetActive(true);
            }
            else if (newPlantState != plantStates.IS_PLANTED)
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
