using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownClock : MonoBehaviour {

    public delegate void TimeEndHandler();
    public static event TimeEndHandler OnTimeEnd;
    public delegate void StartClockHandler(int minutes, int seconds);
    public static event StartClockHandler OnStartClock;
    public delegate void TimeStopHandler();
    public static event TimeStopHandler OnStopClock;

	private Text clock;
	private int currentMin;
	private int currentSec;
    bool started = false;

    void Awake()
    {
        OnStartClock += StartClock;
        OnStopClock += StopClock;
        clock = GetComponentInParent<Text>();
    }

    public static void InitializeAndStart(int minutes, int seconds)
    {
        if (OnStartClock != null)
        {
            OnStartClock(minutes, seconds);
        }
    }
	
    void StartClock(int minutes, int seconds)
    {
        currentMin = minutes;
        currentSec = seconds;
        started = true;
        InvokeRepeating ("ClockTick", 1f, 1f);
    }

    public static void RequestStopClock()
    {
        if (OnStopClock != null)
        {
            OnStopClock();
        }
    }

    void StopClock()
    {
        CancelInvoke("ClockTick");
        enabled = false;
    }

	// Update is called once per frame
	void ClockTick () {
		if (currentMin == 0 && currentSec == 0) {
            CancelInvoke();
            if (OnTimeEnd != null)
            {
                OnTimeEnd();
            }
            return;
		}
		if (currentSec == 0) {
			--currentMin;
			currentSec = 59;
		} else {
			--currentSec;
		}
	}

    void Update()
    {
        if (started)
        {
            SetTime();
        }
    }

    void OnDestroy()
    {
        OnStartClock -= StartClock;
        OnStopClock -= StopClock;
    }

	void SetTime()
	{
		clock.text = System.String.Format("{0}:{1}", currentMin.ToString ("D2"), currentSec.ToString ("D2"));
		if (currentMin == 0 && currentSec <= 15) {
			clock.color = Color.red;
		}
	}

    public int GetRemainingMin()
    {
        return currentMin;
    }

    public int GetRemainingSec()
    {
        return currentSec;
    }
}
