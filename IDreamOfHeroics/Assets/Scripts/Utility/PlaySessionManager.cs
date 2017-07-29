using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySessionManager : MonoBehaviour {

    public const int SCENE_MENU = 0;
    public const int SCENE_FALL = 1;
    public const int SCENE_CHASE = 2;
    public const int SCENE_SURVIVAL = 3;
    public const int SCENE_NIGHTMARE = 4;
    public const int SCENE_REALITY_MOTIVATION = 5;
    public const int SCENE_REALITY_SKILL = 6;
    public const int SCENE_REALITY_SUCCESS = 7;
    public const int SCENE_REALITY_FAILURE = 8;
    public const int SCENE_REALITY_START = 9;

    public static PlaySessionManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoad;
    }
        
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene (sceneIndex);
    }

    void OnSceneLoad(Scene s, LoadSceneMode mode)
    {
        int difficulty = PlayerPrefsManager.LoadStageDifficulty();
        switch (s.name)
        {
            case "MainMenu":
                break;
            case "RealityStart":
                PlayerPrefsManager.ResetLevelNumber();
                PlayerPrefsManager.ResetMotivationGain();
                PlayerPrefsManager.ResetSkillCount();
                PlayerPrefsManager.ResetStageDifficulty();
                PlayerPrefsManager.ResetTotalMotivation();
                break;
            case "Fall":
                FallLevelManager.StartLevel(difficulty);
                break;
            case "Chase":
                break;
            case "Survival":
                break;
            case "Nightmare":
                break;
            case "RealityMotivation":
                PlayerPrefsManager.IncreaseLevelNumber();
                break;
            case "RealitySkill":
                break;
            case "RealitySuccess":
                break;
            case "RealityFail":
                break;
        }
    }

    public void CompleteDream(float hit, float miss)
    {
        if (Mathf.Approximately(hit, 0f) && Mathf.Approximately(miss, 0f))
        {
            Debug.LogError("Could not load level.");
            SceneManager.LoadScene(SCENE_MENU);
            return;
        }

        float motivationGainMultiplier = 1f;
        if (hit < miss)
        {
            motivationGainMultiplier = 0.5f;
        }
        else if (hit >= 3 * miss)
        {
            motivationGainMultiplier = 1.5f;
        }
        motivationGainMultiplier *= 100f;

        float motivationGain = hit * motivationGainMultiplier / (hit + miss);
        int finalGain = Mathf.FloorToInt(motivationGain);

        PlayerPrefsManager.SaveMotivationGain(finalGain);
        ShowReality(SCENE_REALITY_MOTIVATION);
    }

    public void ShowNextDream()
    {
        int levelNumber = PlayerPrefsManager.LoadLevelNumber();
        if (levelNumber < 4)
        {
            switch (levelNumber)
            {
                case 1:
                    SceneManager.LoadScene(SCENE_FALL);
                    break;
                case 2:
                    SceneManager.LoadScene(SCENE_CHASE);
                    break;
                case 3:
                    SceneManager.LoadScene(SCENE_SURVIVAL);
                    break;
            }
        }
        else if (levelNumber % 4 == 0)
        {
            SceneManager.LoadScene(SCENE_NIGHTMARE);
        }
        else
        {
            
        }
    }

    public void ShowReality(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
