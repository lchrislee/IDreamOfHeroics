using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NightmarePlayer : MonoBehaviour {

	public float speed = 10;

	float xMove;
	float zMove;

	// Use this for initialization
	void Start () {
        transform.LookAt(Vector3.zero);
	}
	
	// Update is called once per frame
	void Update () {
		xMove = Input.GetAxis ("Horizontal");
		zMove = Input.GetAxis ("Vertical");

        bool doesX = !Mathf.Approximately(xMove, 0f);
        bool doesZ = !Mathf.Approximately(zMove, 0f);
        if (doesX || doesZ)
        {
            Vector3 movement = Camera.main.transform.forward;
            Debug.Log("Raw Movement: " + movement.ToString());
            movement.y = 0f;
            movement.Normalize();
            Debug.Log("Stripped Movement: " + movement.ToString());
            movement.x *= xMove;
            movement.z *= zMove;
            movement *= speed * Time.deltaTime;
            Debug.Log("Applied Movement: " + movement.ToString());
            transform.Translate(movement);
        }            
	}
}
