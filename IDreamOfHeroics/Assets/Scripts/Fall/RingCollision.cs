using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCollision : MonoBehaviour {
	private bool collided = false;

	void OnTriggerExit(Collider other)
	{
		if (!collided) {
			collided = true;
			RingManager.Hit ();
			Destroy (this.gameObject);
		}
	}

}
