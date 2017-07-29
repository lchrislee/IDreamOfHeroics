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

    public const int FINAL_NIGHTMARE_LEVEL = 16;

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
                ChaseLevelManager.StartLevel(difficulty);
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

    // For use by Fall.
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
        UpdateMotivationReality(motivationGain);
    }

    // For use by Chase.
    public void CompleteDream(
                                bool didExit, 
                                int secondsLeft, 
                                int secondsTotal, 
                                float progress
                             )
    {
        float motivationGainMultiplier = 1f;
        if (!didExit)
        {
            motivationGainMultiplier = 0.5f;
        }
        else if (secondsLeft > (secondsTotal / 2))
        {
            motivationGainMultiplier = 1.5f;
        }
        float motivationGain = 200f;
        motivationGain *= progress * motivationGainMultiplier;
        UpdateMotivationReality(motivationGain);
    }

    // For use by Survival.
    public void CompleteDream(bool didWin, int killCount)
    {
        float motivationGainMultiplier = 1f;
        if (!didWin)
        {
            motivationGainMultiplier = 0.5f;
        }
        else if (killCount > 20)
        {
            motivationGainMultiplier = 1.5f;
        }
        float motivationGain = 200f;
        motivationGain *= motivationGainMultiplier;
        UpdateMotivationReality(motivationGain);
    }

    // For use by Nightmare.
    public void CompleteDream(bool didWin)
    {
        float motivationGainMultiplier = 1f;
        if (!didWin)
        {
            motivationGainMultiplier = 0.5f;
        }
        float motivationGain = 200;
        motivationGain *= motivationGainMultiplier;
        UpdateMotivationReality(motivationGain);
    }

    void UpdateMotivationReality(float motivation)
    {
        int finalGain = Mathf.FloorToInt(motivation);
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
        int motivation = PlayerPrefsManager.LoadTotalMotivation();
        int level = PlayerPrefsManager.LoadLevelNumber();
        if ((motivation > 1000 || level >= FINAL_NIGHTMARE_LEVEL)
            && scene != SCENE_REALITY_FAILURE
            && scene != SCENE_REALITY_SUCCESS
        )
        {
            ShowEnd();
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }

    public void ShowEnd()
    {
        int motivation = PlayerPrefsManager.LoadTotalMotivation();
        if (motivation < 500)
        {
            ShowReality(SCENE_REALITY_FAILURE);
        }
        else
        {
            ShowReality(SCENE_REALITY_SUCCESS);
        }
    }

    public void ShowMainMenu()
    {
        SceneManager.LoadScene(SCENE_MENU);
    }
}
