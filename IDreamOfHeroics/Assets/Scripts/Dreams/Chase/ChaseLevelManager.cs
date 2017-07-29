using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseLevelManager : MonoBehaviour {

    // Objects
    public ChasePlayer player;
    public CountdownClock clock;
    public ChaseGoal goal;
    public ChaseDoom doomWall;

    // Level Data
    ChaseLevelData levelData;

    // Scene Data
    int difficultyLevel;
    float levelProgress = 0f;
    Vector2 goalPosition;
    Vector2 originalPosition;
    float originalDistance = 0f;

    // Events
    public delegate void LevelStartHandler(int difficulty);
    public static event LevelStartHandler OnLevelStart;
    public delegate void LevelProgressHandler();
    public static event LevelProgressHandler OnLevelProgress;
    public delegate void LevelEndHandler(bool completed);
    public static event LevelEndHandler OnLevelEnd;


    void Awake()
    {
        CountdownClock.OnTimeEnd += EndScene;
        OnLevelStart += SetupLevel;
        OnLevelProgress += LevelProgress;
        OnLevelEnd += CompleteAndEnd;
    }

    public static void StartLevel(int difficulty)
    {
        if (OnLevelStart != null)
        {
            OnLevelStart(difficulty);
        }
    }

    void SetupLevel(int difficulty)
    {
        difficultyLevel = difficulty;
        levelData = ResourceManager.LoadChaseLevel(difficultyLevel);
        if (levelData == null)
        {
            PlaySessionManager.instance.CompleteDream(false, 0, 0, 0f);
        }
        player.speed = levelData.playerSpeed;
        goalPosition = new Vector2(
            goal.transform.position.x,
            goal.transform.position.z);
        originalPosition = new Vector2(
            player.transform.position.x,
            player.transform.position.z);
        originalDistance = Vector2.Distance(
            goalPosition, 
            originalPosition);

        float playerZ = player.transform.position.z;
        float wallSize = doomWall.GetComponent<Renderer>().bounds.size.z / 2;
        Vector3 newDoomPosition = doomWall.transform.position;
        newDoomPosition.z = playerZ - levelData.wallStartDistance;
        newDoomPosition.z -= wallSize;
        doomWall.transform.position = newDoomPosition;
        doomWall.speed = levelData.wallSpeed;

        CountdownClock.InitializeAndStart(
            levelData.timerMinutes,
            levelData.timerSeconds);
    }

    public static void SetLevelProgress()
    {
        if (OnLevelProgress != null)
        {
            OnLevelProgress();
        }
    }
	
    public void LevelProgress()
    {
        Vector2 playerPosition = new Vector2(
            player.transform.position.x,
            player.transform.position.z);

        float distanceRemaining = Vector2.Distance(
            playerPosition, 
            goalPosition);
        
        levelProgress = (originalDistance - distanceRemaining);
        levelProgress /= originalDistance;
    }

    public static void CompleteScene()
    {
        if (OnLevelEnd != null)
        {
            OnLevelEnd(true);
        }
    }

    public static void StopScene()
    {
        if (OnLevelEnd != null)
        {
            OnLevelEnd(false);
        }
    }

    void EndScene()
    {
        CompleteAndEnd(false);
    }

    void CompleteAndEnd(bool completed)
    {
        CountdownClock.RequestStopClock();
        CountdownClock.OnTimeEnd -= EndScene;
        player.CancelInvoke();
        player.enabled = false;
        PlaySessionManager.instance.CompleteDream(
            completed,
            clock.GetRemainingMin() * 60 + clock.GetRemainingSec(),
            levelData.timerMinutes * 60 + levelData.timerSeconds,
            levelProgress
        );
    }
}
