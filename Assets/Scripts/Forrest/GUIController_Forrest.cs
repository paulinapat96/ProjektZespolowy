using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIController_Forrest : MonoBehaviour
{

    [SerializeField] private Image hpBar;
    
    private void Start()
    {
        Garden.OnHpChange += ChangeHealthBar;
    }

    public void ChangeHealthBar(float value)
    {
        hpBar.fillAmount = value;
    }

    public void OnClickButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
}
