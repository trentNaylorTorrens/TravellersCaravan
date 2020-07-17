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
    }

    private void OnDisable()
    {
        EventManager.OnPlayButton -= SidePanelOpen;
        EventManager.PMOnBackToMainMenuButton -= SidePanelClose;
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
