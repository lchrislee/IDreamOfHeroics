using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMovement : MonoBehaviour {

	private bool markedForDeletion = false;
	public float movementSpeed = 7f;
	public float maxY = 3f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void LateUpdate () {
		if (transform.position.y > maxY && !markedForDeletion) {
			markedForDeletion = true;
			RingManager.Miss ();
			Destroy (this.gameObject);
		} else {
			Vector3 movement = new Vector3 (0, movementSpeed, 0);
			movement *= Time.deltaTime;
			transform.position += movement;
		}
	}
}
