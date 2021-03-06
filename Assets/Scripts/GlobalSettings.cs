﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    public static GlobalSettings Instance;
    
    [Header("Difficulty Settings")]
    public float TutorialDifficultyTimer = 20000f;
    public float EasyDifficultyTimer = 500f;
    public float MediumDifficultyTimer = 400f;
    public float HardDifficultyTimer = 500f;
    public float NightMareDifficultyTimer = 200f;

    public static bool gameIsReplay = false;

    //Save references
    public const string LEVELDIFFICULTY = "LevelDifficulty";
    public const string MUSICVOLUME = "MusicVolume";
    public const string EFFECTSVOLUME = "EffectsVolume";

    public void Awake()
    {
        if(Instance != null)
        { 
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

   
}
