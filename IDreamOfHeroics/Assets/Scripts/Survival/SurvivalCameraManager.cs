using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalCameraManager : MonoBehaviour {

    public float hSensitivity = 30f;
    public float vSensitivity = 20f;

    float CameraVerticalMin = 0f;
    float CameraVerticalMax = 60f;
	
	// Update is called once per frame
	void Update () {
        float hVel = Input.GetAxis("Mouse X") * Time.deltaTime * hSensitivity;
        transform.Rotate(new Vector3(0f, hVel, 0f));


        float vVel = Input.GetAxis("Mouse Y") * Time.deltaTime * vSensitivity;
        float existingV = Camera.main.transform.rotation.eulerAngles.x;
        float vertical = existingV + vVel;
        vertical = Mathf.Clamp(vertical, CameraVerticalMin, CameraVerticalMax);
        Camera.main.transform.Rotate(new Vector3(vertical - existingV, 0f, 0f));
	}
}
