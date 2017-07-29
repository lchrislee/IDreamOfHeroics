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
        // TODO: REMOVE THIS and fill in proper skill choices from player data.
        Object[] skills = Resources.LoadAll("Skill", typeof(SkillData));
        SkillData[] chosenSkills = new SkillData[2];
        for (int i = 0; i < chosenSkills.Length; ++i)
        {
            chosenSkills[i] = (SkillData) skills[Random.Range(0, skills.Length)];
        }
        uiSkillManager.InitializeAndEnable(chosenSkills);

        // Input.
        playerClass = (BattleClass) Resources.LoadAll("Battle Class", typeof(BattleClass))[1];
        inputManager.InitializeAndMonitor(playerClass, chosenSkills);

        // Health.
        // TODO: Retain player data so I can put in the defense.
        healthManager.InitializePlayerStats(levelData.playerHp, 0);

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
