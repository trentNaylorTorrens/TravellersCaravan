using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();    
        
    }

    private void OnEnable()
    {
        EventManager.OnPlayButton += MainMenuToPlay;
        EventManager.OnSettingsButton += AnyToSettings;
        EventManager.SMOnBackToMainMenuButton += SettingsToAny;
        EventManager.PMOnBackToMainMenuButton += PauseToMainMenu;
        EventManager.OnRestartLevel += SnapToGame;
        EventManager.OnCreditButton += MainMenuToCredits;
    }

    private void OnDisable()
    {
        EventManager.OnPlayButton -= MainMenuToPlay;
        EventManager.OnSettingsButton -= AnyToSettings;
        EventManager.SMOnBackToMainMenuButton -= SettingsToAny;
        EventManager.PMOnBackToMainMenuButton -= PauseToMainMenu;
        EventManager.OnRestartLevel -= SnapToGame;
        EventManager.OnCreditButton -= MainMenuToCredits;

    }

    void SnapToGame()
    {
        myAnimator.SetTrigger("SnapToGame");
    }

    void MainMenuToPlay()
    {
        myAnimator.SetBool("Game", true);
        myAnimator.SetBool("MainMenu", false);
    }

    void AnyToSettings()
    {
        if (GameManager.instance.currentLevelState == GameManager.LevelState.Pregame)
        {
            myAnimator.SetBool("Settings", true);
            myAnimator.SetBool("MainMenu", false);
        }
        else if (GameManager.instance.currentLevelState == GameManager.LevelState.Paused)
        {
            myAnimator.SetBool("Settings", true);
            myAnimator.SetBool("Game", false);
        }
    }

    void PauseToMainMenu()
    {
        myAnimator.SetBool("MainMenu", true);
        myAnimator.SetBool("Game", false);
        myAnimator.SetBool("Settings", false);
    }

    void MainMenuToCredits()
    {
        myAnimator.SetBool("MainMenu", true);
        myAnimator.SetBool("Game", false);
        myAnimator.SetBool("Settings", false);
    }

    void SettingsToAny()
    {
        if (GameManager.instance.currentLevelState == GameManager.LevelState.Pregame)
        {
            myAnimator.SetBool("MainMenu", true);
            myAnimator.SetBool("Game", false);
            myAnimator.SetBool("Settings", false);
        }
        else if (GameManager.instance.currentLevelState == GameManager.LevelState.Paused)
        {
            myAnimator.SetBool("MainMenu", false);
            myAnimator.SetBool("Game", true);
            myAnimator.SetBool("Settings", false);
        }
    }
}
