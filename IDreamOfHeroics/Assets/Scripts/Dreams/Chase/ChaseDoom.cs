using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseDoom : MonoBehaviour {

    public ChasePlayer player;

    public float speed = 4f;

    bool didTrigger = false;

    void OnTriggerEnter()
    {
        if (!didTrigger)
        {
            didTrigger = true;
            Invoke("TriggerDeath", 1f);

        }
    }

    void TriggerDeath()
    {
        ChaseLevelManager.StopScene();
    }

    void Update()
    {
        Vector3 movement = player.transform.position - transform.position;
        movement.Normalize();
        movement *= Time.deltaTime * speed;
        transform.position += movement;
    }
}
