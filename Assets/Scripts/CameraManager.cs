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
        EventManager.OnSettingsButton += MainMenuToSettings;
        EventManager.SMOnBackToMainMenuButton += SettingsToMainMenu;
        EventManager.PMOnBackToMainMenuButton += PauseToMainMenu;
    }

    private void OnDisable()
    {
        
    }

    void MainMenuToPlay()
    {
        myAnimator.SetBool("Game", true);
        myAnimator.SetBool("MainMenu", false);
    }

    void MainMenuToSettings()
    {
        myAnimator.SetBool("Settings", true);
        myAnimator.SetBool("MainMenu", false);
    }

    void PauseToMainMenu()
    {
        myAnimator.SetBool("MainMenu", true);
        myAnimator.SetBool("Game", false);
        myAnimator.SetBool("Settings", false);
    }

    void SettingsToMainMenu()
    {
        myAnimator.SetBool("MainMenu", true);
        myAnimator.SetBool("Game", false);
        myAnimator.SetBool("Settings", false);
    }
}
