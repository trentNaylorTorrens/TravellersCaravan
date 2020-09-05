using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RatBoyManager : MonoBehaviour
{
    public GameObject MainMenuRatBoy; 
    public TMP_Text MainMenuDialogueBox;
    public List<string> MainMenuDialogue;

    public GameObject GameRatBoy;
    public TMP_Text GameDialogueBox;
    public List<string> GameDialogue;

    private void OnEnable()
    {
        EventManager.OnPauseButton += RatBoyFadeOut;
        
        EventManager.OnResumePlayButton += RatBoyFadeIn;

        EventManager.OnPatternMatch += RatBoyPatternMatch;
        EventManager.OnPatternMisMatch += RayBoyPatternMisMatch;
        EventManager.OnGameOver += RatboyGameOver;
    
    }

    private void OnDisable()
    {
        EventManager.OnPauseButton -= RatBoyFadeOut;
        
        EventManager.OnResumePlayButton -= RatBoyFadeIn;


        EventManager.OnPatternMatch -= RatBoyPatternMatch;
        EventManager.OnPatternMisMatch -= RayBoyPatternMisMatch;
    }

    void RatBoyFadeOut()
    {
        GameRatBoy.SetActive(false);
        GameDialogueBox.gameObject.SetActive(false);
    }

    void RatBoyFadeIn()
    {
        GameRatBoy.SetActive(true);
        GameDialogueBox.gameObject.SetActive(true);
    }

    void RatboyGameOver(bool val)
    {
        if(val)
        {
            UpdateGameDialogue(4);
        }
        else 
        {
            UpdateGameDialogue(5);

        }    
    }
 
    void RatBoyPatternMatch()
    {
        UpdateGameDialogue(2);
    }

    void RayBoyPatternMisMatch()
    {
        UpdateGameDialogue(3);
    }

    public void UpdateGameDialogue(int diagEntry)
    {
        GameDialogueBox.text = GameDialogue[diagEntry];
    }

}
