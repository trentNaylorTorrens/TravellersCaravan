using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public delegate void PatternMatchAction();
    public static event PatternMatchAction OnPatternMatch;

    public delegate void PatternMismatchAction();
    public static event PatternMismatchAction OnPatternMisMatch;

    public delegate void GameOverAction(bool isWinner);
    public static event GameOverAction OnGameOver;
    private void Awake()
    {
        instance = this;
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
}
