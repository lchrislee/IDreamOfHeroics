using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Game Data/Skill")]
public class Skill : ScriptableObject {

    public Texture2D icon;
    public string skillName;
    public string skillDescription;
    public float cooldownTime;
    public float effectTime;
    public int damage;
    public int damageModifier;
    public int gameObjectAffectCount;
}
