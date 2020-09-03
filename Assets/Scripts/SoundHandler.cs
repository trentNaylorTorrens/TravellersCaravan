using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundHandler : MonoBehaviour
{
    private AudioSource audioSource;

    public bool dontDestroy = false;
    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if(dontDestroy)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        
    }
    public void PlayOnce(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void Play(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
