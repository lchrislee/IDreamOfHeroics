using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCollision : MonoBehaviour {
	private bool collided = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit(Collider other)
	{
		if (!collided) {
			collided = true;
			RingManager.Hit ();
			Destroy (this.gameObject);
		}
	}

}
