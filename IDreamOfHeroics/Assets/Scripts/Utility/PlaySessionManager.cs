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
    public const int REALITY_SKILL_SCENE = 5;
    public const int REALITY_MOTIVATION_SCENE = 6;
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


    void OnSceneLoad(Scene s, LoadSceneMode mode)
    {
        if (s.name == "Fall")
        {
            LevelManager.StartLevel();
        }
    }

    public void LoadReality(float hit, float miss)
    {
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
        Debug.Log("Hit: " + hit + ", missed: " + miss + ", total: " + (hit + miss));
        Debug.Log("Gained " + finalGain + " motivation.");
        SceneManager.LoadScene(MENU_SCENE);
    }
}
