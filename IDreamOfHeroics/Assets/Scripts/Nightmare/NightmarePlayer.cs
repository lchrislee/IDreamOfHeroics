using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class NightmarePlayer : MonoBehaviour {

	private CharacterController cc;
	public float speed;

	float xMove;
	float zMove;

	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		xMove = Input.GetAxis ("Horizontal");
		zMove = Input.GetAxis ("Vertical");

		float newXFacing = Mathf.LerpAngle (transform.forward.x, 1f, xMove);
		float newZFacing = Mathf.LerpAngle (transform.forward.z, 1f, zMove);
		transform.LookAt (new Vector3 (newXFacing, 0f, newZFacing));

		Vector3 movement = Vector3.zero;
		if (xMove != 0 || zMove != 0) {
			movement.x = xMove * speed * transform.forward.x;
			movement.z = zMove * speed * transform.forward.z;
		}
		movement = transform.TransformDirection (movement);
		cc.SimpleMove (movement);
	}
}
