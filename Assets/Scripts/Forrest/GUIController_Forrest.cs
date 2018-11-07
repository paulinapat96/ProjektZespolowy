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
        Time.timeScale = 0;

        SceneManager.LoadScene(0);
        
    }
    
    private void OnDestroy()
    {
        Garden.OnHpChange -= ChangeHealthBar;
    }

}
