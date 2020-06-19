using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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
    }

    private void OnEnable()
    {
        EventManager.OnGameOver += WinGameScreen;
    }

    private void OnDisable()
    {
        EventManager.OnGameOver -= WinGameScreen;
    }
    void WinGameScreen(bool isWinner)
    {
        testWinText.text = "You are winner = " + isWinner.ToString();
        testWinText.gameObject.SetActive(true);
    }
}
