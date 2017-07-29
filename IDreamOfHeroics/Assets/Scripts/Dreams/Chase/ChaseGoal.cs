using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseGoal : MonoBehaviour {

    bool didSend = false;

    void OnTriggerEnter()
    {
        if (!didSend)
        {
            didSend = true;
            ChaseLevelManager.CompleteScene();
        }
    }
}
