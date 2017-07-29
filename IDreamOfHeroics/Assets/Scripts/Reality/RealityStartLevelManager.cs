using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityStartLevelManager : MonoBehaviour {

    public void Complete()
    {
        PlaySessionManager.instance.ShowNextDream();
    }
}
