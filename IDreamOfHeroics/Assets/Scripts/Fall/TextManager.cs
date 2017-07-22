using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

	public Text prompt;
	public Text ringsHit;
	public Text ringsMissed;

	// Use this for initialization
	void Start () {
		Invoke ("ExtendPrompt", 5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (CountdownClock.IsCompleted) {
			prompt.text = "Wake up...";
		}
	}

	void LateUpdate()
	{
		ringsHit.text = "Caught: " + RingManager.collidedRingCount;
		ringsMissed.text = "Missed: " + RingManager.missedRingCount;
	}

	void ExtendPrompt(){
		prompt.text = "Falling...again...";
	}
}
