using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityEndLevelManager : MonoBehaviour {

    public void CompleteLevel()
    {
        PlaySessionManager.instance.ShowMainMenu();
    }
}
