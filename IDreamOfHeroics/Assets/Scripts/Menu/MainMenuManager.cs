using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    public GameObject debugButton;

	// Use this for initialization
	void Start () {
#if UNITY_EDITOR
        debugButton.SetActive(true);
#else
        debugButton.SetActive(false);
#endif
	}
	
    public void NewGame()
    {
        PlaySessionManager.instance.ShowReality(
            PlaySessionManager.SCENE_REALITY_START);
    }

    public void LoadScene(int scene)
    {
        PlaySessionManager.instance.LoadScene(scene);
    }
}
