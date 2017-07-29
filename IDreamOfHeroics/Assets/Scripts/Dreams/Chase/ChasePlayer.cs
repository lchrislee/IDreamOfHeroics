using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour {

    public float speed = 10f;

    void Start()
    {
        InvokeRepeating("SendProgressUpdate", 0.5f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 velocity = Vector3.zero;
        if (!Mathf.Approximately(Input.GetAxis("Vertical"), 0f))
        {
            velocity.z -= Input.GetAxis("Vertical");
        }
        if (!Mathf.Approximately(Input.GetAxis("Horizontal"), 0f))
        {
            velocity.x -= Input.GetAxis("Horizontal");
        }
        velocity *= Time.deltaTime * speed;
        transform.position += velocity;
        Camera.main.transform.position += velocity;
	}

    void SendProgressUpdate()
    {
        ChaseLevelManager.SetLevelProgress();
    }
}
