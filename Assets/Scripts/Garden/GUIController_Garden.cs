using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIController_Garden : MonoBehaviour
{

    // Use this for initialization

    public static event Action<int> OnClick;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickPlantButton(int type)
    {
        if (OnClick != null) OnClick(type);
    }
}
