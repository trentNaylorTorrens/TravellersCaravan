using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Renderer myRend;

    private void Awake()
    {
        myRend = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevealBox()
    {
        AudioManager.Instance.PlaySoundEffectOneShot(this.gameObject, AudioManager.Instance.boxOpening);
        iTween.ValueTo(gameObject, iTween.Hash("from", 0.70f, "to", 1f, "time", 0.5f, "onupdatetarget", gameObject, "onupdate", "DissolveOnUpdateCallBack"));
        
    }

    public void CloseBox()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0.7f, "time", 1f, "onupdatetarget", gameObject, "onupdate", "DissolveOnUpdateCallBack", "easetype", iTween.EaseType.easeInCirc));
        AudioManager.Instance.PlaySoundEffectOneShot(this.gameObject, AudioManager.Instance.boxClosing);
    }

    void DissolveOnUpdateCallBack(float nValue)
    {
        myRend.material.SetFloat("_Amount", nValue);
    }
   
}
