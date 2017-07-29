using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {
    public const string NEW_MOTIVATION_GAIN = "NEW_MOTIVATION_GAIN";
    public const string TOTAL_MOTIVATION = "TOTAL_MOTIVATION";
    public const string STAGE_DIFFICULTY = "STAGE_DIFFICULTY";
    public const string SELECTED_CLASS = "SELECTED_CLASS";
    public const string LEVEL_NUMBER = "LEVEL_NUMBER";
    public const string SKILL_COUNT = "SKILL_COUNT";
    public const string SKILL_A = "SKILL_A";
    public const string SKILL_B = "SKILL_B";
    public const string SKILL_C = "SKILL_C";

    // Motivation

    public static void ResetMotivationGain()
    {
        PlayerPrefs.SetInt(NEW_MOTIVATION_GAIN, 0);
    }

    public static void SaveMotivationGain(int newGain)
    {
        PlayerPrefs.SetInt(NEW_MOTIVATION_GAIN, newGain);
    }

    public static int LoadMotivationGain()
    {
        return PlayerPrefs.GetInt(NEW_MOTIVATION_GAIN, 0);
    }

    public static void ResetTotalMotivation()
    {
        PlayerPrefs.SetInt(TOTAL_MOTIVATION, 0);
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

    public static void ResetLevelNumber()
    {
        PlayerPrefs.SetInt(LEVEL_NUMBER, 1);
    }

    public static void IncreaseLevelNumber()
    {
        PlayerPrefs.SetInt(LEVEL_NUMBER, LoadLevelNumber() + 1);
    }

    public static int LoadLevelNumber()
    {
        return PlayerPrefs.GetInt(LEVEL_NUMBER, 1);
    }

    // Skills

    public static void ResetSkillCount()
    {
        PlayerPrefs.SetInt(SKILL_COUNT, 0);
    }

    public static void SaveSkillCount(int count)
    {
        PlayerPrefs.SetInt(SKILL_COUNT, count);
    }

    public static int LoadSkillCount()
    {
        return PlayerPrefs.GetInt(SKILL_COUNT, 0);
    }

    public static SkillData[] LoadPlayerSkills()
    {
        int count = LoadSkillCount();
        SkillData[] output = new SkillData[count];
        if (count >= 1)
        {
            output[0] = LoadSkillA();
            if (count >= 2)
            {
                output[1] = LoadSkillB();
                if (count == 3)
                {
                    output[2] = LoadSkillC();
                }
            }
        }
        return output;
    }

    public static void SaveSkill(SkillData skill, int count)
    {
        if (count == 0)
        {
            SaveSkillA(skill);
        }
        else if (count == 1)
        {
            SaveSkillB(skill);
        }
        else if (count == 2)
        {
            SaveSkillC(skill);
        }
    }

    public static void SaveSkillA(SkillData data)
    {
        PlayerPrefs.SetString(SKILL_A, data.skillName);
    }

    public static void SaveSkillB(SkillData data)
    {
        PlayerPrefs.SetString(SKILL_B, data.skillName);
    }

    public static void SaveSkillC(SkillData data)
    {
        PlayerPrefs.SetString(SKILL_C, data.skillName);
    }

    public static SkillData LoadSkillA()
    {
        string name = PlayerPrefs.GetString(SKILL_A);
        return ResourceManager.LoadSkill(name);
    }

    public static SkillData LoadSkillB()
    {
        string name = PlayerPrefs.GetString(SKILL_B);
        return ResourceManager.LoadSkill(name);
    }

    public static SkillData LoadSkillC()
    {
        string name = PlayerPrefs.GetString(SKILL_C);
        return ResourceManager.LoadSkill(name);
    }

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

    public static void ResetStageDifficulty()
    {
        PlayerPrefs.SetInt(STAGE_DIFFICULTY, 1);
    }

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
