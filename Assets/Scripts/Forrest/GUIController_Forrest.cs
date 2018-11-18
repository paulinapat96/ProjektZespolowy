using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIController_Forrest : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Image hpBar;

    private bool isGamePaused = false;
    
    private void Start()
    {
        Player.OnHpChange += ChangeHealthBar;
        
        isGamePaused = false;
        pausePanel.SetActive(isGamePaused);
        
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
    
    public void OnClickPauseButton()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1;
            isGamePaused = false;
            pausePanel.SetActive(isGamePaused);
        }
        else
        {
            Time.timeScale = 0;
            isGamePaused = true;
            pausePanel.SetActive(isGamePaused);
        }

        
    }

    
    private void OnDestroy()
    {
        Player.OnHpChange -= ChangeHealthBar;
    }

}
