using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalLevelManager : MonoBehaviour {

    // Object Data
    public SurvivalPlayer player;
    public Transform enemyFolder;

    // Level Data
    public SurvivalLevelData levelData;

    // Scene Data
    public SurvivalEnemySpawnManager spawnManager;
    public SurvivalHealthManager healthManager;

    // Delegates and Events
    public delegate void LevelStartHandler();
    public static event LevelStartHandler OnLevelStart;
    public delegate void LevelEndHandler();
    public static event LevelEndHandler OnLevelStop;

    void Awake()
    {
        OnLevelStart += SetupLevel;
        OnLevelStop += EndScene;
        CountdownClock.OnTimeEnd += EndScene;
    }

	// Use this for initialization
	void Start () {
        StartLevel();
	}

    public static void StartLevel()
    {
        if (OnLevelStart != null)
        {
            OnLevelStart();
        }
    }

    void SetupLevel()
    {
        int maxHp = levelData.playerHp;
        healthManager.InitializePlayerStats(maxHp);
        spawnManager.InitializeAndStartSpawning(
            levelData.typeToSpawn, 
            levelData.respawnTime);
        CountdownClock.InitializeAndStart(
            levelData.timerMinutes, 
            levelData.timerSeconds);
    }

    public static void StopScene()
    {
        if (OnLevelStop != null)
        {
            OnLevelStop();
        }
    }

    void EndScene()
    {
        SurvivalEnemySpawnManager.DisableSpawning();
        SurvivalEnemyAI.DisableAttack();
        foreach (Transform enemy in enemyFolder)
        {
            enemy.GetComponent<SurvivalEnemyAI>().enabled = false;
        }
        player.enabled = false;
        CountdownClock.RequestStopClock();
        CountdownClock.OnTimeEnd -= EndScene;
    }

}
