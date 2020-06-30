using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/NewLevel", order = 1)]
public class Level : ScriptableObject
{
   
    public enum LevelState { Pregame, Playing, GameOver };
    [Header("Level Settings")]
    public LevelState currentLevelState = LevelState.Pregame;
    
    public enum LevelDifficulty {Tutorial, Easy, Medium, Hard, Nightmare };
    public LevelDifficulty currentLevelDifficulty = LevelDifficulty.Easy;
}
