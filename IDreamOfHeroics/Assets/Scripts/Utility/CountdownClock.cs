using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownClock : MonoBehaviour {

	public static bool IsCompleted = false;

	public int startingMinutes;
	public int startingSeconds;

	private Text clock;
	private int currentMin;
	private int currentSec;

	// Use this for initialization
	void Start () {
		clock = GetComponentInParent<Text>();
		currentMin = startingMinutes;
		currentSec = startingSeconds;
		SetTime ();
		InvokeRepeating ("UpdateText", 1f, 1f);
	}
	
	// Update is called once per frame
	void UpdateText () {
		if (currentMin == 0 && currentSec == 0) {
			IsCompleted = true;
			return;
		}
		if (currentSec == 0) {
			--currentMin;
			currentSec = 59;
		} else {
			--currentSec;
		}
		SetTime ();
	}

	void SetTime()
	{
		clock.text = System.String.Format("{0}:{1}", currentMin.ToString ("D2"), currentSec.ToString ("D2"));
		if (currentMin == 0 && currentSec <= 10) {
			clock.color = Color.red;
		}
	}
}
