using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Level Data/Nightmare Boss Data")]
public class BossData : ScriptableObject {

    public string bossName;
    public int maxHealth = 100;
    public float attackRate = 1f;
    public int strength = 5;
    public int defense = 0;
}
