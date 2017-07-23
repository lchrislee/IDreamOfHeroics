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
		LevelManager.OnScoreUpdate += UpdateScoreText;
        CountdownClock.OnTimeEnd += ShowEndText;
	}

	void ExtendPrompt(){
		prompt.text = "Falling...again...";
	}
	
	void ShowEndText () {
		prompt.text = "Wake up...";
	}

	void UpdateScoreText(int caught, int missed)
	{
		ringsHit.text = "Caught: " + caught;
		ringsMissed.text = "Missed: " + missed;
	}
}
