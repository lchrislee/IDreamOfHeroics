using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public GameObject player;

	private float yOffset;

	// Use this for initialization
	void Start () {
		yOffset = transform.position.y - player.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate(){
		Vector3 newPosition = transform.position;
		newPosition.y = player.transform.position.y + yOffset;
		transform.position = newPosition;
	}
}
