using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Data File/Level Data/Falling Level Data")]
public class FallingLevelData : ScriptableObject {

    public int maxConcurrentRings;
    public float ringScale = 1f;
    public float ringSpread = 1f;
    public float ringMovementSpeed = 7f;
    public int timerMinutes = 0;
    public int timerSeconds = 30;
}
