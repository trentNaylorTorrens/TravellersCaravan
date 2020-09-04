using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator sidePanelAnimator;
    public Animator caravanAnimator;
    public Animator hourGlassAnimator;

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
        EventManager.OnPlayButton += SidePanelOpen;
        EventManager.PMOnBackToMainMenuButton += SidePanelClose;
        EventManager.OnReplayButton += SidePanelClose;
        EventManager.OnRestartLevel += SidePanelOpen;
        EventManager.OnPlayButton += TimerStart;
        EventManager.OnRestartLevel += TimerStart;
    }

    private void OnDisable()
    {
        EventManager.OnPlayButton -= SidePanelOpen;
        EventManager.PMOnBackToMainMenuButton -= SidePanelClose;
        EventManager.OnReplayButton -= SidePanelClose;
        EventManager.OnRestartLevel -= SidePanelOpen;
        EventManager.OnPlayButton -= TimerStart;
        EventManager.OnRestartLevel -= TimerStart;
    }

    void SidePanelOpen()
    {
        sidePanelAnimator.SetTrigger("Open");
    }

    void SidePanelClose()
    {
        sidePanelAnimator.SetTrigger("Close");
    }

    void TimerStart()
    {
        hourGlassAnimator.SetTrigger("StartTimer");
    }
}
