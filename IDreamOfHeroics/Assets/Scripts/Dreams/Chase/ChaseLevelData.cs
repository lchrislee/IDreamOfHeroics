using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Level Data/Chase Level Data")]
public class ChaseLevelData : ScriptableObject {

    public int timerMinutes = 0;
    public int timerSeconds = 30;
    public float playerSpeed = 10f;
    public float wallSpeed = 4f;
    public float wallStartDistance = 60f;
}
