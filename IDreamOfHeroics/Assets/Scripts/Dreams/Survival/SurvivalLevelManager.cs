using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalLevelManager : MonoBehaviour {

    // Object Data
    public SurvivalCameraManager player;
    public Transform enemyFolder;

    // Level Data
    public SurvivalLevelData levelData;
    public BattleClass playerClass;

    // Scene Data
    public SurvivalEnemySpawnManager spawnManager;
    public HealthUIManager healthManager;
    public BattleSkillUIManager uiSkillManager;
    public SurvivalInputManager inputManager;

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
        // Skills.
        SkillData[] chosenSkills = PlayerPrefsManager.LoadPlayerSkills();
        uiSkillManager.InitializeAndEnable(chosenSkills);

        // Input.
        BattleClassType typeToUse = PlayerPrefsManager.LoadSelectedClass();
        playerClass = ResourceManager.LoadBattleClass(typeToUse);
        Debug.Log("class selected: " + playerClass.className);
        inputManager.InitializeAndMonitor(playerClass, chosenSkills);

        // Health.
        healthManager.InitializeStats(levelData.playerHp, playerClass.def);

        // Enemy.
        spawnManager.InitializeAndStartSpawning(
            levelData.typeToSpawn, 
            levelData.respawnTime);

        // Clock.
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
