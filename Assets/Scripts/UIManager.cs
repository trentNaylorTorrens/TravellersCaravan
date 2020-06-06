using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text testWinText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        testWinText.gameObject.SetActive(true);
    }
}
