using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Game Data/Skill")]
public class SkillData : ScriptableObject {

    public static string[] skillNames = new string[]{
        "Armor", 
        "Chain",
        "Cleave",
        "FireR",
        "Heal",
        "Invis",
        "Pierce",
        "Rage",
        "UNFINISHED"
    };

    public Texture2D icon;
    public string skillName;
    public string skillDescription;
    public float cooldownTime;
    public float effectTime;
    public int damage;
    public int damageModifier;
    public int gameObjectAffectCount;
    public int skillTier;
    public BattleClass owningClass;
}
