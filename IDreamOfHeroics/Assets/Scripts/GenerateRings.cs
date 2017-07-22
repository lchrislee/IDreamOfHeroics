using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRings : MonoBehaviour {

	public Vector3 minPositionRange;
	public Vector3 maxPositionRange;
	public Transform ring;
	public GameObject player;

	// Use this for initialization
	void Start () {
		Vector3 playerPosition = player.transform.position;
		Quaternion facingUp = Quaternion.identity;
		facingUp.eulerAngles = new Vector3 (90, 0, 0);
		for (int i = 0; i < 20; ++i) {
			float x = Random.Range (minPositionRange.x, maxPositionRange.x);
			float y = Random.Range (minPositionRange.y, maxPositionRange.y);
			float z = Random.Range (minPositionRange.z, maxPositionRange.z);
			Vector3 location = new Vector3(x, y, z);
			Instantiate (ring, location, facingUp);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
