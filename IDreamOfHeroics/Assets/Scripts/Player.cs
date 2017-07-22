using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {

	CharacterController cc;
	public float speed;

	float xMove;
	float zMove;

	void Awake(){
		cc = GetComponent<CharacterController> ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		xMove = Input.GetAxis ("Horizontal");
		zMove = Input.GetAxis ("Vertical");
		Vector3 movement = Vector3.zero;
		if (xMove != 0 || zMove != 0) 
		{
			movement.x = xMove * speed;
			movement.z = zMove * speed;
		}
		movement = transform.TransformDirection (movement) * Time.deltaTime;
		cc.Move(movement);
	}
}
