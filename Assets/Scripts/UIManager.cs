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
    public GameObject UIP_EndGameMenu;

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
        EventManager.OnPlayButton += MM_PlayButtonPressed;
        EventManager.OnPauseButton += PM_PauseButtonPressed;
        EventManager.SMOnBackToMainMenuButton += SM_BackToMainMenuButtonPressed;
        EventManager.PMOnBackToMainMenuButton += PM_BackToMainMenuButtonPressed;
        EventManager.OnSettingsButton += MM_SettingsButtonPressed;
        EventManager.OnResumePlayButton += PM_ResumeButtonPressed;
        EventManager.OnQuitButton += MM_QuitButtonPressed;
        EventManager.OnQuitGameButton += PM_QuitButton;
        EventManager.OnRestartLevel += MM_PlayButtonPressed;
        EventManager.OnReplayButton += EG_ReplayButtonPressed;
    }

    private void OnDisable()
    {
        EventManager.OnGameOver -= WinGameScreen;
        EventManager.OnPlayButton -= MM_PlayButtonPressed;
        EventManager.OnPauseButton -= PM_PauseButtonPressed;
        EventManager.SMOnBackToMainMenuButton -= SM_BackToMainMenuButtonPressed;
        EventManager.PMOnBackToMainMenuButton -= PM_BackToMainMenuButtonPressed;
        EventManager.OnSettingsButton -= MM_SettingsButtonPressed;
        EventManager.OnResumePlayButton -= PM_ResumeButtonPressed;
        EventManager.OnQuitButton -= MM_QuitButtonPressed;
        EventManager.OnQuitGameButton -= PM_QuitButton;
        EventManager.OnRestartLevel -= MM_PlayButtonPressed;
        EventManager.OnReplayButton -= EG_ReplayButtonPressed;
    }
    void WinGameScreen(bool isWinner)
    {
        UIP_EndGameMenu.SetActive(true);
        UIP_SettingsMenu.SetActive(false);
        GameManager.instance.playerCanInput = false;
        testWinText.text = "You are winner = " + isWinner.ToString();
    }
    
    void MM_PlayButtonPressed()
    {
        UIP_MainMenu.SetActive(false);
        GameManager.instance.playerCanInput = true;
    }
    void MM_SettingsButtonPressed()
    {
        UIP_SettingsMenu.SetActive(true);
        UIP_MainMenu.SetActive(false);
    }
    void MM_QuitButtonPressed()
    {
        Application.Quit();
    }

    void PM_ResumeButtonPressed()
    {
        UIP_PauseMenu.SetActive(false);
        GameManager.instance.playerCanInput = true;
    }

    void PM_SettingsButtonPressed()
    {
        GameManager.instance.playerCanInput = false;
    }
    void PM_QuitButton()
    {
       
    }

    void PM_BackToMainMenuButtonPressed()
    {
        UIP_SettingsMenu.SetActive(false);
        UIP_PauseMenu.SetActive(false);
        UIP_MainMenu.SetActive(true);
        GameManager.instance.playerCanInput = false;
    }
    void SM_BackToMainMenuButtonPressed()
    {
        UIP_SettingsMenu.SetActive(false);
        UIP_PauseMenu.SetActive(false);
        UIP_MainMenu.SetActive(true);
    }
    void PM_PauseButtonPressed()
    {
        GameManager.instance.playerCanInput = UIP_PauseMenu.activeSelf;
        UIP_PauseMenu.SetActive(!UIP_PauseMenu.activeSelf);
        GameManager.instance.PauseGame();
    }

    void EG_ReplayButtonPressed()
    {
        GlobalSettings.gameIsReplay = true;
        UIP_EndGameMenu.SetActive(false);
        GameManager.instance.QuitGame();
    }
   
   
}
