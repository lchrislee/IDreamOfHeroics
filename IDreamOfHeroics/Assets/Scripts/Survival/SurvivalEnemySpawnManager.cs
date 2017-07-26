using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Spider,
    Zombie
}

public struct LocationAndRotation
{
    public Vector3 location;
    public Vector3 rotation;
}

public class SurvivalEnemySpawnManager : MonoBehaviour {

    public SurvivalCameraManager player;
    public Transform spiderPrefab;
    public Transform zombiePrefab;

    float respawnCooldown;
    EnemyType enemyTypeToUse;

    LocationAndRotation[] potentialSpawnPoints = new LocationAndRotation[4];

    public delegate void StopSpawningHandler();
    public static event StopSpawningHandler OnStopSpawning;

    void Awake()
    {
        potentialSpawnPoints[0].location = new Vector3(0f, 0.25f, 34.5f);
        potentialSpawnPoints[0].rotation = new Vector3(0f, 180f, 0);
        potentialSpawnPoints[1].location = new Vector3(0f, 0.25f, -34.5f);
        potentialSpawnPoints[1].rotation = new Vector3(0f, 0f, 0f);
        potentialSpawnPoints[2].location = new Vector3(34.5f, 0.25f, 0f);
        potentialSpawnPoints[2].rotation = new Vector3(0f, -90f, 0f);
        potentialSpawnPoints[3].location = new Vector3(-34.5f, 0.5f, 0f);
        potentialSpawnPoints[3].rotation = new Vector3(0f, 90f, 0f);
        OnStopSpawning += StopSpawning;
    }

    public void InitializeAndStartSpawning(
        EnemyType typeToUse, 
        float cooldown)
    {
        enemyTypeToUse = typeToUse;
        respawnCooldown = cooldown;
        InvokeRepeating("SpawnEnemy", 3f, respawnCooldown);
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, potentialSpawnPoints.Length);
        Vector3 spawnLocation = potentialSpawnPoints[randomIndex].location;
        Quaternion spawnRotation = Quaternion.identity;
        spawnRotation.eulerAngles = potentialSpawnPoints[randomIndex].rotation;
        Transform prefabToUse;
        switch (enemyTypeToUse)
        {
            case EnemyType.Zombie:
                prefabToUse = zombiePrefab;
                break;
            default:
                prefabToUse = spiderPrefab;
                break;
        }
        Instantiate(prefabToUse, spawnLocation, spawnRotation, transform);
    }

    void StopSpawning()
    {
        CancelInvoke("SpawnEnemy");
    }

    public static void DisableSpawning()
    {
        if (OnStopSpawning != null)
        {
            OnStopSpawning();
        }
    }
}
