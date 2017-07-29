using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealityMotivationLevelManager : MonoBehaviour {

    public Text oldMotivation;
    public Text addMotivation;
    public Text totalMotivation;

	// Use this for initialization
	void Start () {
        int old = PlayerPrefsManager.LoadTotalMotivation();
        oldMotivation.text = old.ToString();
        int add = PlayerPrefsManager.LoadMotivationGain();;
        addMotivation.text = add.ToString();
        totalMotivation.text = "+" + (old + add).ToString();
        PlayerPrefsManager.SaveMotivationGain(add);
        PlayerPrefsManager.IncreaseLevelNumber();
	}

    public void CompleteLevel()
    {
        PlaySessionManager.instance.ShowReality(
            PlaySessionManager.SCENE_REALITY_SKILL);
    }
}
