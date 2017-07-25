using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

	private const float Y_ANGLE_MIN = -5f;
	private const float Y_ANGLE_MAX = 45f;

	public Transform lookAt;
	public Transform cameraTransform;

	float distanceToTarget = 6f;
	float currentX;
	float currentY;

	void Start () {
		cameraTransform = transform;
	}

	void Update()
	{
		currentX += Input.GetAxis ("Mouse X");
		currentY -= Input.GetAxis ("Mouse Y");

		currentY = Mathf.Clamp (currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
	}
		
	void LateUpdate () {
		Vector3 direction = new Vector3 (0, 0, -distanceToTarget);
		Quaternion rotation = Quaternion.Euler (currentY, currentX, 0);
		cameraTransform.position = lookAt.position + rotation * direction;
		cameraTransform.LookAt (lookAt);
	}
}
