using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public delegate void BoxInteract();
    public static event BoxInteract OnBoxOpen;
    public static event BoxInteract OnBoxClose;

    public delegate void PatternMatchAction();
    public static event PatternMatchAction OnPatternMatch;

    public delegate void PatternMismatchAction();
    public static event PatternMismatchAction OnPatternMisMatch;

    public delegate void GameOverAction(bool isWinner);
    public static event GameOverAction OnGameOver;

    public delegate void GameRestartAction();
    public static event GameRestartAction OnRestartLevel;

    public delegate void DifficultySettingChange(GameManager.LevelDifficulty nDiff);
    public static event DifficultySettingChange OnDifficultyChange;

    //UI EVENTS//

    public delegate void UIButton();
    public static event UIButton OnPlayButton;
    public static event UIButton OnPauseButton;
    public static event UIButton OnSettingsButton;
    public static event UIButton SMOnBackToMainMenuButton;
    public static event UIButton PMOnBackToMainMenuButton;
    public static event UIButton OnResumePlayButton;
    public static event UIButton OnQuitGameButton;
    public static event UIButton OnCreditButton;

    //End Game UI
    public static event UIButton OnReplayButton;

    private void Awake()
    {
        instance = this;
    }
   
    public void BoxOpen()
    {
        if(OnBoxOpen != null)
        {
            OnBoxOpen();
        }
    }

    public void BoxClose()
    {
        if (OnBoxOpen != null)
        {
            OnBoxOpen();
        }
    }
    public void PatternMatch()
    {
        if(OnPatternMatch != null)
        {
            OnPatternMatch();
        }
    }

    public void PatternMisMatch()
    {
        if (OnPatternMisMatch != null)
        {
            OnPatternMisMatch();
        }
    }

    public void GameOver(bool isWin)
    {
        if(OnGameOver != null)
        {
            OnGameOver(isWin);
        }
    }

    public void RestartedLevel()
    {
        if(OnRestartLevel != null)
        {
            OnRestartLevel();
        }
    }
    //Public UI Events//
    public void UIPlayButton()
    { 
        if(OnPlayButton != null)
        {
            OnPlayButton();
        }
    }

    public void UIPauseButton()
    {
        if (OnPauseButton != null)
        {
            OnPauseButton();
        }
    }

    public void UISettingsButton()
    {
        if (OnSettingsButton != null)
        {
            OnSettingsButton();
        }
    }
    public void UIBackToMainMenuButton()
    {
        if (SMOnBackToMainMenuButton != null)
        {
            SMOnBackToMainMenuButton();
        }
    }
    public void PMBackToMainMenuButton()
    {
        if (PMOnBackToMainMenuButton != null)
        {
            PMOnBackToMainMenuButton();
        }
    }

    public void UIResumePlayButton()
    {
        if (OnResumePlayButton != null)
        {
            OnResumePlayButton();
        }
    }

    public void UIReplayButton()
    {
        if (OnReplayButton != null)
        {
            OnReplayButton();
        }
    }
   
    public void UICreditButton()
    {
        if (OnReplayButton != null)
        {
            OnReplayButton();
        }
    }
   

    //Quit the current game.
    public void UiQuitGameButton()
    {
        if (OnQuitGameButton != null)
        {
            OnQuitGameButton();
        }
    }

    public void SettingsChangeDifficulty(GameManager.LevelDifficulty nDiff)
    {
        if(OnDifficultyChange != null)
        {
            OnDifficultyChange(nDiff);
        }
    }
}
