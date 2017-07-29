using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FallingPlayer : MonoBehaviour {

	CharacterController cc;
	public float speed;

	float xMove;
	float zMove;

	void Awake(){
		cc = GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update () {
        xMove = Input.GetAxis ("Horizontal");
        zMove = Input.GetAxis ("Vertical");
        Vector3 movement = Vector3.zero;
        if (xMove != 0 || zMove != 0) {
            movement.x = xMove;
            movement.z = zMove;
        }
        movement = movement.normalized * speed;
        movement = transform.TransformDirection (movement) * Time.deltaTime;
        cc.Move (movement);
	}
}
