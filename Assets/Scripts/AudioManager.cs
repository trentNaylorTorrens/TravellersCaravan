using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioMixer audioMixer;

    [Header("Game Event Sound")]
    public AudioClip correctMatch;
    public AudioClip incorrectMatch;
    public AudioClip winMatch;
    public AudioClip losematch;

    public AudioClip menuMusic;
    public AudioClip gameplayMusic;

    [Header("UI Sound")]
    public AudioClip sliderChange;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        } 
    }

    public void PlaySoundEffectOneShot(GameObject objectPlayingSound, AudioClip clipToPlay)
    { 
        if(objectPlayingSound.GetComponent<SoundHandler>() != null)
        {
            GetComponent<SoundHandler>().PlayOnce(clipToPlay);
        }
    }

    public void PlaySoundEffect(GameObject objectPlayingSound, AudioClip clipToPlay)
    {
        if (objectPlayingSound.GetComponent<SoundHandler>() != null)
        {
            GetComponent<SoundHandler>().Play(clipToPlay);
        }
    }

    //Generic Sound Functions

    public void PlayUISliderEffect()
    {
        PlaySoundEffectOneShot(this.gameObject, sliderChange);
    }

    //Retrieved from - https://answers.unity.com/questions/283192/how-to-convert-decibel-number-to-audio-source-volu.html
    public static float LinearToDecibel(float linear)
    {
        float dB;

        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;

        return dB;
    }

    public static float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20.0f);

        return linear;
    }
}
