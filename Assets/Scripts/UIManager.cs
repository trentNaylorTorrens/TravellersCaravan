using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject UIP_MainMenu;
    public GameObject UIP_PauseMenu;
    public GameObject UIP_SettingsMenu;

    public Text testWinText;
    public TMP_Text uiRemainingTime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        uiRemainingTime.text = "Time remaining: " + GameManager.instance.LevelTimer.ToString();

        if(Input.GetKeyDown(KeyCode.Escape) && !UIP_MainMenu.activeSelf)
        {
            EventManager.instance.UIPauseButton();
        }
    }

    private void OnEnable()
    {
        EventManager.OnGameOver += WinGameScreen;
        EventManager.OnPlayButton += MM_PlayButton;
        EventManager.OnPauseButton += PM_PauseButton;
        EventManager.SMOnBackToMainMenuButton += SM_BackToMainMenuButton;
        EventManager.PMOnBackToMainMenuButton += PM_BackToMainMenuButton;
        EventManager.OnSettingsButton += MM_SettingsButton;
        EventManager.OnResumePlayButton += PM_ResumeButton;
        EventManager.OnQuitButton += MM_QuitButton;
        EventManager.OnQuitGameButton += PM_QuitButton;
    }

    private void OnDisable()
    {
        EventManager.OnGameOver -= WinGameScreen;
        EventManager.OnPlayButton -= MM_PlayButton;
        EventManager.OnPauseButton -= PM_PauseButton;
        EventManager.SMOnBackToMainMenuButton -= SM_BackToMainMenuButton;
        EventManager.PMOnBackToMainMenuButton -= PM_BackToMainMenuButton;
        EventManager.OnSettingsButton -= MM_SettingsButton;
        EventManager.OnResumePlayButton -= PM_ResumeButton;
        EventManager.OnQuitButton -= MM_QuitButton;
        EventManager.OnQuitGameButton -= PM_QuitButton;
    }
    void WinGameScreen(bool isWinner)
    {
        testWinText.text = "You are winner = " + isWinner.ToString();
        testWinText.gameObject.SetActive(true);
    }
    
    void MM_PlayButton()
    {
        UIP_MainMenu.SetActive(false);
        GameManager.instance.playerCanInput = true;
    }
    void MM_SettingsButton()
    {
        UIP_SettingsMenu.SetActive(true);
        UIP_MainMenu.SetActive(false);
    }
    void MM_QuitButton()
    {
        Application.Quit();
    }

    void PM_ResumeButton()
    {
        UIP_PauseMenu.SetActive(false);
        GameManager.instance.playerCanInput = true;
    }

    void PM_SettingsButton()
    {
        GameManager.instance.playerCanInput = false;
    }
    void PM_QuitButton()
    {
       
    }

    void PM_BackToMainMenuButton()
    {
        UIP_SettingsMenu.SetActive(false);
        UIP_PauseMenu.SetActive(false);
        UIP_MainMenu.SetActive(true);
        GameManager.instance.playerCanInput = false;
    }
    void SM_BackToMainMenuButton()
    {
        UIP_SettingsMenu.SetActive(false);
        UIP_PauseMenu.SetActive(false);
        UIP_MainMenu.SetActive(true);
    }
    void PM_PauseButton()
    {
        GameManager.instance.playerCanInput = UIP_PauseMenu.activeSelf;
        UIP_PauseMenu.SetActive(!UIP_PauseMenu.activeSelf);
        GameManager.instance.PauseGame();
    }
}
