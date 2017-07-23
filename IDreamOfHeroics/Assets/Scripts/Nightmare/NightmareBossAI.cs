using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareBossAI : MonoBehaviour {

	public Transform playerObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = playerObject.position;
		direction.y = 0;
		transform.forward = direction.normalized;
	}
}
