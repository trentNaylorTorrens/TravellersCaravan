using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Panels")]
    public GameObject UIP_MainMenu;
    public GameObject UIP_PauseMenu;
    public GameObject UIP_SettingsMenu;
    public GameObject UIP_EndGameMenu;
    public GameObject UIP_GameHUD;
    public GameObject UIO_PauseButton;
    public GameObject UIP_CreditMenu;
    public GameObject UIP_MMButton;

    public Text testWinText;
    public TMP_Text uiRemainingTime;

    [Header("Settings")]
    public UnityEngine.UI.Slider difficultySlider;
    public TMP_Text difficultyText;
    public UnityEngine.UI.Slider musicSlider;
    public TMP_Text musicVolText;
    public UnityEngine.UI.Slider soundSlider;
    public TMP_Text soundVolText;

    [Header("Game Over Screen")]
    public GameObject winImage;
    public GameObject loseImage;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        uiRemainingTime.text = "Time remaining: " + GameManager.instance.LevelTimer.ToString();
        if(GameManager.instance.currentLevelState == GameManager.LevelState.Playing || GameManager.instance.currentLevelState == GameManager.LevelState.Paused)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !UIP_MainMenu.activeSelf && !UIP_SettingsMenu.activeSelf)
            {
                EventManager.instance.UIPauseButton();
            }
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
        EventManager.OnQuitGameButton += PM_QuitButton;
        EventManager.OnRestartLevel += MM_PlayButtonPressed;
        EventManager.OnReplayButton += EG_ReplayButtonPressed;
        EventManager.OnCreditButton += MM_CreditButtonPressed;
        EventManager.OnQuitGameButton += CM_QuitButton;
        
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
        EventManager.OnQuitGameButton -= PM_QuitButton;
        EventManager.OnRestartLevel -= MM_PlayButtonPressed;
        EventManager.OnReplayButton -= EG_ReplayButtonPressed;
        EventManager.OnCreditButton -= MM_CreditButtonPressed;
        EventManager.OnQuitGameButton -= CM_QuitButton;

    }
    void WinGameScreen(bool isWinner)
    {
        if(isWinner)
        {
            AudioManager.Instance.PlaySoundEffectOneShot(this.gameObject, AudioManager.Instance.winMatch);
            winImage.SetActive(true);
            loseImage.SetActive(false);
        }
        else
        {
            AudioManager.Instance.PlaySoundEffectOneShot(this.gameObject, AudioManager.Instance.losematch);
            winImage.SetActive(false);
            loseImage.SetActive(true);
        }
        UIP_EndGameMenu.SetActive(true);
        UIP_SettingsMenu.SetActive(false);
        UIP_CreditMenu.SetActive(false);
        UIP_MMButton.SetActive(false);
        GameManager.instance.playerCanInput = false;
        UIManager.Instance.UIO_PauseButton.SetActive(false);
        //testWinText.text = "You are winner = " + isWinner.ToString();
    }
    
    void MM_PlayButtonPressed()
    {
        UIP_MainMenu.SetActive(false);
        UIP_CreditMenu.SetActive(false);
        UIP_MMButton.SetActive(false);
        GameManager.instance.playerCanInput = true;
        AudioManager.Instance.TransistionMusic(AudioManager.Instance.menuMusic, AudioManager.Instance.gameplayMusic);
    }
    void MM_SettingsButtonPressed()
    {
        if (GameManager.instance.currentLevelState == GameManager.LevelState.Pregame)
        {
            UIP_MainMenu.SetActive(false);
            UIP_SettingsMenu.SetActive(true);//needs delay here 
            UIP_CreditMenu.SetActive(false);
            UIP_MMButton.SetActive(false);
        }
        else if (GameManager.instance.currentLevelState == GameManager.LevelState.Paused)
        {
            UIP_SettingsMenu.SetActive(true);
            UIP_PauseMenu.SetActive(false);
            UIP_CreditMenu.SetActive(false);
            UIP_MMButton.SetActive(false);
        }
    }
    public void MM_QuitButtonPressed()
    {
        Application.Quit();
    }

    void PM_ResumeButtonPressed()
    {
        UIP_PauseMenu.SetActive(false);
        UIP_CreditMenu.SetActive(false);
        UIP_MMButton.SetActive(false);
        UIManager.Instance.UIO_PauseButton.SetActive(true);
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
        UIP_GameHUD.SetActive(false);
        UIP_PauseMenu.SetActive(false);
        UIP_MainMenu.SetActive(true);
        UIP_CreditMenu.SetActive(false);
        UIP_MMButton.SetActive(false);
        GameManager.instance.playerCanInput = false;
    }
    void SM_BackToMainMenuButtonPressed()
    {
        UIP_SettingsMenu.SetActive(false);
        if(GameManager.instance.currentLevelState == GameManager.LevelState.Pregame)
        {
            UIP_PauseMenu.SetActive(false);
            UIP_MainMenu.SetActive(true);
            UIP_CreditMenu.SetActive(false);
            UIP_MMButton.SetActive(false);
        }
        else if (GameManager.instance.currentLevelState == GameManager.LevelState.Paused)
        {
            UIP_PauseMenu.SetActive(true);
        }
    }
    void PM_PauseButtonPressed()
    {
        GameManager.instance.playerCanInput = UIP_PauseMenu.activeSelf;
        UIP_PauseMenu.SetActive(!UIP_PauseMenu.activeSelf);
        UIManager.Instance.UIO_PauseButton.SetActive(false);
        UIManager.Instance.difficultySlider.interactable = !UIP_PauseMenu.activeSelf;
        if (UIP_SettingsMenu.activeSelf)
        {
            UIP_SettingsMenu.SetActive(false);
            UIP_PauseMenu.SetActive(false);
            UIP_CreditMenu.SetActive(false);
            UIP_MMButton.SetActive(false);
            UIManager.Instance.UIP_GameHUD.SetActive(true);
        }
        else 
        {
        
        }
        
        GameManager.instance.PauseGame();

    }

    void EG_ReplayButtonPressed()
    {
        GlobalSettings.gameIsReplay = true;
        UIP_EndGameMenu.SetActive(false);
        UIP_CreditMenu.SetActive(false);
        UIP_MMButton.SetActive(false);
        GameManager.instance.QuitGame();
    }

    void MM_CreditButtonPressed()
    {
        UIP_SettingsMenu.SetActive(false);
        UIP_GameHUD.SetActive(false);
        UIP_PauseMenu.SetActive(false);
        UIP_MainMenu.SetActive(false);
        UIP_CreditMenu.SetActive(true);
        UIP_MMButton.SetActive(true);
        GameManager.instance.playerCanInput = false;
    }

    void CM_QuitButton()
    {

    }

    //Settings Menu
    public void OnDifficultyChange()
    {
        int diff = (int)UIManager.Instance.difficultySlider.value;
        GameManager.LevelDifficulty newLevelDifficulty = (GameManager.LevelDifficulty)diff;
        EventManager.instance.SettingsChangeDifficulty(newLevelDifficulty);
        difficultyText.text = GameManager.instance.currentLevelDifficulty.ToString();
       
    }

    public void OnMusicVolumeChange()
    {
        //Play sound
       
        musicVolText.text = musicSlider.value.ToString();
        //convert slider to volume dB
        // float db = (-80 + ((musicSlider.value / 100) * 80));

        float db = AudioManager.LinearToDecibel(musicSlider.value);
        
        AudioManager.Instance.audioMixer.SetFloat("MusicVolume", db-40);

    }

    public void OnSoundVolumeChange()
    {
      
        soundVolText.text = soundSlider.value.ToString();
        //convert slider to volume dB
        // float db = (-80 + ((soundSlider.value / 100) * 80));
        float db = AudioManager.LinearToDecibel(soundSlider.value);
        AudioManager.Instance.audioMixer.SetFloat("EffectsVolume", db-40);
    }
   
}
