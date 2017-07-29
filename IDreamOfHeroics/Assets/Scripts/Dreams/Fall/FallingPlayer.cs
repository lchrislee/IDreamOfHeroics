using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlayer : MonoBehaviour {

	public float speed;

	float xMove;
	float yMove;

	// Update is called once per frame
	void Update () {
        xMove = Input.GetAxis ("Horizontal");
        yMove = Input.GetAxis ("Vertical");
        Vector3 movement = Vector3.zero;
        if (!Mathf.Approximately(xMove, 0f) 
            || !Mathf.Approximately(yMove, 0f)) {
            movement.x = xMove;
            movement.y = yMove;
        }
        movement = movement.normalized * speed;
        movement = transform.TransformDirection (movement) * Time.deltaTime;
        transform.position += movement;
	}
}
