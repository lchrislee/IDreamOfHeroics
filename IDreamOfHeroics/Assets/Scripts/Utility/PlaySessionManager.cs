using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySessionManager : MonoBehaviour {

    public const int MENU_SCENE = 0;
    public const int FALL_SCENE = 1;
    public const int CHASE_SCENE = 2;
    public const int SURVIVAL_SCENE = 3;
    public const int NIGHTMARE_SCENE = 4;
    public const int REALITY_MOTIVATION_SCENE = 5;
    public const int REALITY_SKILL_SCENE = 6;
    public const int REALITY_SUCCESS_SCENE = 7;
    public const int REALITY_FAIL_SCENE = 8;

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
        if (s.name == "Fall")
        {
            int difficulty = PlayerPrefsManager.LoadStageDifficulty();
            FallLevelManager.StartLevel(difficulty);
        }
    }

    public void LoadReality(float hit, float miss)
    {
        if (Mathf.Approximately(hit, 0f) && Mathf.Approximately(miss, 0f))
        {
            Debug.LogError("Could not load level.");
            SceneManager.LoadScene(MENU_SCENE);
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
        SceneManager.LoadScene(MENU_SCENE);
    }
}
