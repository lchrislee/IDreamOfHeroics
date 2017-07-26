using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Game Data/Battle Class")]
public class BattleClass : ScriptableObject {

    public string className;
    public string classDescription;
    public int def;
    public bool canRange;
    public bool canMelee;
    public float attackCooldown;
    public float attackDamage;
}
