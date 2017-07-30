using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareLevelManager : MonoBehaviour {

    // Objects
    public NightmarePlayer player;
    public BossHealthManager bossHealth;

    // Level Data
    BossData bossData;

    // Scene Data
    int difficultyLevel;

    // Events
    public delegate void LevelStartHandler(int difficulty);
    public static event LevelStartHandler OnLevelStart;
    public delegate void LevelEndHandler(bool didWin);
    public static event LevelEndHandler OnLevelEnd;

    void Awake()
    {
        OnLevelStart += SetupLevel;
        OnLevelEnd += EndLevel;
    }

    public static void StartLevel(int difficulty)
    {
        if (OnLevelStart != null)
        {
            OnLevelStart();
        }
    }

    void SetupLevel(int difficulty)
    {
        difficultyLevel = difficulty;
        bossData = ResourceManager.LoadBossLevel(difficulty);
        if (bossData == null)
        {
            EndWith(false);
            return;
        }
        bossHealth.InitializeAndStart(bossData);
    }

    public static void EndWith(bool didWin)
    {
        if (OnLevelEnd != null)
        {
            OnLevelEnd(didWin);
        }
    }

    void EndLevel(bool didWin)
    {
        PlaySessionManager.instance.CompleteDream(didWin);
    }
}
