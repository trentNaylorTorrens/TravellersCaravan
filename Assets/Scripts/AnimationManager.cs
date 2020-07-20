using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator sidePanelAnimator;
    public Animator caravanAnimator;

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
    }

    private void OnDisable()
    {
        EventManager.OnPlayButton -= SidePanelOpen;
        EventManager.PMOnBackToMainMenuButton -= SidePanelClose;
        EventManager.OnReplayButton -= SidePanelClose;
        EventManager.OnRestartLevel -= SidePanelOpen;
    }

    void SidePanelOpen()
    {
        sidePanelAnimator.SetTrigger("Open");
    }

    void SidePanelClose()
    {
        sidePanelAnimator.SetTrigger("Close");
    }
}
