using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallLevelManager : MonoBehaviour {

	// Objects
	public FallingPlayer player;
	public Transform ringFolder;
	public Transform ringPrefab;

	// Level Data
    FallingLevelData levelData;

	// Scene Data
    int difficultyLevel;
	static int collidedRingCount = 0;
	static int missedRingCount = 0;
    public Vector3 minPositionRange;
    public Vector3 maxPositionRange;
    Quaternion facingUp;

	// Events
    public delegate void LevelStartHandler(int difficulty);
	public static event LevelStartHandler OnLevelStart;
	public delegate void UpdateScoreHandler(int caught, int missed);
	public static event UpdateScoreHandler OnScoreUpdate;
    delegate void RingHandler();
    static event RingHandler OnRingDestroyed;

	void Awake()
	{
        // Establish SetupLevel as a OnLevelStart handler that receives event.
		OnLevelStart += SetupLevel;
        OnRingDestroyed += AddRing;
        CountdownClock.OnTimeEnd += EndScene;

		facingUp = Quaternion.identity;
		facingUp.eulerAngles = new Vector3 (90, 0, 0);
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
        levelData = ResourceLoadManager.LoadFallLevel(difficultyLevel);

        if (levelData == null)
        {
            PlaySessionManager.instance.LoadReality(0, 0);
            return;
        }
		ClearRings();
        RingMovement.movementSpeed = levelData.ringMovementSpeed;
		player.transform.position = new Vector3(-1f, 0f, 1f);
		
		// Generate Rings
        for (int i = 0; i < levelData.maxConcurrentRings; ++i) {
			AddRing();
		}

        CountdownClock.InitializeAndStart(
            levelData.timerMinutes, 
            levelData.timerSeconds);
	}

	void ClearRings() {
		foreach (Transform child in ringFolder) {
			Destroy (child.gameObject);
		}
	}

	void AddRing()
	{
		float x = Random.Range (minPositionRange.x, maxPositionRange.x);
		float y = Random.Range (minPositionRange.y, maxPositionRange.y);
		float z = Random.Range (minPositionRange.z, maxPositionRange.z);
		Vector3 location = new Vector3 (x, y, z);
		Transform newRing = Instantiate (ringPrefab, location, facingUp);
        newRing.transform.localScale *= levelData.ringScale;
		newRing.parent = ringFolder;
	}

	public static void RingHit(Transform ring){
		++collidedRingCount;
		Destroy(ring.gameObject);
		if (OnScoreUpdate != null)
		{
			OnScoreUpdate(collidedRingCount, missedRingCount);
		}
        OnRingDestroyed();
	}

	public static void RingMiss(Transform ring)
	{
		++missedRingCount;
		Destroy(ring.gameObject);
		if (OnScoreUpdate != null)
		{
			OnScoreUpdate(collidedRingCount, missedRingCount);
		}
        OnRingDestroyed();
	}

    void EndScene()
    {
        player.enabled = false;
        foreach (Transform child in ringFolder)
        {
            RingMovement movement = child.GetComponent<RingMovement>();
            movement.enabled = false;
        }
        CountdownClock.OnTimeEnd -= EndScene;
        PlaySessionManager.instance
            .LoadReality((float) collidedRingCount, (float) missedRingCount);
    }
}
