using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerClass
{
    Wizard,
    Knight,
    Rogue
}

public class SurvivalInputManager : MonoBehaviour {

    PlayerClass classToMonitor;

	// Use this for initialization
	void Start () {
		
	}

    public void InitializeAndMonitor(PlayerClass playerClass)
    {
        classToMonitor = playerClass;
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
