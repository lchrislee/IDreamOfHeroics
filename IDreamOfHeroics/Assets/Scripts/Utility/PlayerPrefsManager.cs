using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {
    public const string NEW_MOTIVATION_GAIN = "NEW_MOTIVATION_GAIN";
    public const string TOTAL_MOTIVATION = "TOTAL_MOTIVATION";
    public const string STAGE_DIFFICULTY = "STAGE_DIFFICULTY";
    public const string SELECTED_CLASS = "SELECTED_CLASS";
    public const string LEVEL_NUMBER = "LEVEL_NUMBER";

    // Motivation

    public static void SaveMotivationGain(int newGain)
    {
        PlayerPrefs.SetInt(NEW_MOTIVATION_GAIN, newGain);
    }

    public static int LoadMotivationGain()
    {
        return PlayerPrefs.GetInt(NEW_MOTIVATION_GAIN, 0);
    }

    public static void UpdateTotalMotivation(int changeMotivationBy)
    {
        
        int newTotal = LoadTotalMotivation() + changeMotivationBy;
        PlayerPrefs.SetInt(TOTAL_MOTIVATION, newTotal);
    }

    public static int LoadTotalMotivation()
    {
        return PlayerPrefs.GetInt(TOTAL_MOTIVATION, 0);
    }

    // Level

    public static void IncreaseLevelNumber()
    {
        PlayerPrefs.SetInt(LEVEL_NUMBER, LoadLevelNumber() + 1);
    }

    public static int LoadLevelNumber()
    {
        return PlayerPrefs.GetInt(LEVEL_NUMBER, 1);
    }

    // Skills



    // Class

    public static void SaveSelectedClass(BattleClassType typeToSave)
    {
        PlayerPrefs.SetInt(
            SELECTED_CLASS, 
            BattleClass.GetIntFromType(typeToSave)
        );
    }

    public static BattleClassType LoadSelectedClass()
    {
        return BattleClass.GetTypeFromInt(PlayerPrefs.GetInt(SELECTED_CLASS));
    }

    // Difficulty

    public static void IncreaseStageDifficulty()
    {
        int newDifficulty = LoadStageDifficulty() + 1;
        PlayerPrefs.SetInt(STAGE_DIFFICULTY, newDifficulty);
    }

    public static int LoadStageDifficulty()
    {
        return PlayerPrefs.GetInt(STAGE_DIFFICULTY, 1);
    }
}
