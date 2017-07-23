using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMovement : MonoBehaviour {

	private bool markedForDeletion = false;
	public static float movementSpeed;
	
    float deletionHeight = 3f;

	// Update is called once per frame
	void LateUpdate () {
		if (transform.position.y > deletionHeight && !markedForDeletion)
        {
			markedForDeletion = true;
			LevelManager.RingMiss(this.transform);
        } 
        else
        {
			Vector3 movement = new Vector3 (0, movementSpeed, 0);
			movement *= Time.deltaTime;
			transform.position += movement;
		}
	}
}
