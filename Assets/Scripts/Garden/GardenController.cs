using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenController : MonoBehaviour
{
    public enum plantStates
    {
        IS_PICKED,
        IS_BEING_PLANTED,
        IS_PLANTED
    }
    [SerializeField] private List<GameObject> plantPrefs = new List<GameObject>();
    private List<GameObject> plants = new List<GameObject>();
    [SerializeField] private List<GameObject> tile = new List<GameObject>();
    public static bool[,] grid = new bool[11, 11];
    public static int goldCount;
    [SerializeField] private Text goldDisplay;
    plantStates newPlantState;

    void Start()
    {
        goldCount = 0;
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

        GUIController_Garden.OnClick += SpawnPlant;
    }

    void Update()
    {
        goldDisplay.text = goldCount.ToString();
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
        plants[plants.Count-1].GetComponent<Plant>().OnDestroyPlant += HarvestPlant;
    }

    private void HarvestPlant(GameObject obj)
    {
        for (var i = 0; i < plants.Count; i++)
        {
            if (plants[i] == obj)
            {
                Plant plant = plants[i].GetComponent<Plant>();
                plant.OnDestroyPlant -= HarvestPlant;
                plants.RemoveAt(i);
                Destroy(obj);
                break;
            }
        }
    }

    private void OnDestroy()
    {
        GUIController_Garden.OnClick -= SpawnPlant;
    }
}
