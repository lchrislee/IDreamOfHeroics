using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public static FallingLevelData LoadFallLevel(int difficulty)
    {
        string fileName = "Level Data/Fall/Fall " + difficulty;
        return Resources.Load<FallingLevelData>(fileName);
    }

    public static SurvivalLevelData LoadSurvivalLevel(int difficulty)
    {
        string fileName = "Level Data/Survival/Survival " + difficulty;
        return Resources.Load<SurvivalLevelData>(fileName);
    }

    public static ChaseLevelData LoadChaseLevel(int difficulty)
    {
        string fileName = "Level Data/Chase/Chase " + difficulty.ToString();
        return Resources.Load<ChaseLevelData>(fileName);
    }

    public static BossData LoadBossLevel(int difficulty)
    {
        string fileName = "Level Data/Nightmare/Nightmare " 
            + difficulty.ToString();
        return Resources.Load<BossData>(fileName);
    }

    public static SkillData LoadSkill(string name)
    {
        string fileName = "Skill/" + name;
        return Resources.Load<SkillData>(fileName);
    }

    public static SkillData[] LoadSkillTier(int tier)
    {
        string fileName = "Skill";
        SkillData[] allSkills = Resources.LoadAll<SkillData>(fileName);
        List<SkillData> output = new List<SkillData>();
        foreach (SkillData skill in allSkills)
        {
            if (skill.skillTier == tier)
            {
                output.Add(skill);
            }
        }

        return output.ToArray();
    }

    public static BattleClass[] LoadBattleClasses()
    {
        return Resources.LoadAll<BattleClass>("Battle Class");
    }

    public static BattleClass LoadBattleClass(BattleClassType type)
    {
        string fileName = "Battle Class/";
        switch (type)
        {
            case BattleClassType.MAGE:
                fileName += "Mage";
                break;
            case BattleClassType.ROGUE:
                fileName += "Rogue";
                break;
            case BattleClassType.WARRIOR:
                fileName += "Warrior";
                break;
        }
        return Resources.Load<BattleClass>(fileName);
    }
}
