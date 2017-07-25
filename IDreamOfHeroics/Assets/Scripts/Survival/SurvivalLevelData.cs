using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Data File/Level Data/Survival Level Data")]
public class SurvivalLevelData : ScriptableObject {
    public int timerMinutes = 0;
    public int timerSeconds = 30;
    public int playerHp = 100;
    public EnemyType typeToSpawn = EnemyType.Spider;
    public float respawnTime = 2f;
}
