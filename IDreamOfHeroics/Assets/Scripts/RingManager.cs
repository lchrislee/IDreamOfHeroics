using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingManager : MonoBehaviour {

	public Vector3 minPositionRange;
	public Vector3 maxPositionRange;
	public int maxConcurrentRings = 3;
	public Transform ring;
	public GameObject player;

	public static int currentRings;

	// Use this for initialization
	void Start () {
		currentRings = 0;
		AddRings ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentRings < maxConcurrentRings) {
			AddRings ();
		}
	}

	void AddRings()
	{
		Quaternion facingUp = Quaternion.identity;
		facingUp.eulerAngles = new Vector3 (90, 0, 0);
		for (int i = currentRings; i < maxConcurrentRings; ++i) {
			float x = Random.Range (minPositionRange.x, maxPositionRange.x);
			float y = Random.Range (minPositionRange.y, maxPositionRange.y);
			float z = Random.Range (minPositionRange.z, maxPositionRange.z);
			Vector3 location = new Vector3 (x, y, z);
			Instantiate (ring, location, facingUp);
		}
		currentRings = maxConcurrentRings;
	}
}
