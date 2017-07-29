using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleClassType
{
    MAGE,
    WARRIOR,
    ROGUE
}

[CreateAssetMenu(menuName="Game Data/Battle Class")]
public class BattleClass : ScriptableObject {

    public string className;
    public string classDescription;
    public int def;
    public bool canRange;
    public bool canMelee;
    public float attackCooldown;
    public float attackDamage;
    public BattleClassType type;

    public static BattleClassType GetTypeFromInt(int type)
    {
        switch (type)
        {
            case 1:
                return BattleClassType.MAGE;
            case 2:
                return BattleClassType.WARRIOR;
            default:
                return BattleClassType.ROGUE;
        }
    }

    public static int GetIntFromType(BattleClassType type)
    {
        switch (type)
        {
            case BattleClassType.MAGE:
                return 1;
            case BattleClassType.WARRIOR:
                return 2;
            default:
                return 3;
        }
    }
}
