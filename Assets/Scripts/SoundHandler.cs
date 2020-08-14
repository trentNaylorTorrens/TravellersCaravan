using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundHandler : MonoBehaviour
{
    private AudioSource audioSource;
    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
