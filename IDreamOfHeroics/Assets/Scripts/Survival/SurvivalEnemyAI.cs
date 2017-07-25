using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class SurvivalEnemyAI : MonoBehaviour {

    float moveSpeed = 4f;
    float attackRate = 1f;
    bool atTarget = false;
    bool mustWaitForClear = false;
    int strength = 2;

    public delegate void StopAttackHandler();
    public static event StopAttackHandler OnStopAttack;

	// Use this for initialization
	void Start () {
        OnStopAttack += StopAttack;
	}
	
	// Update is called once per frame
	void Update () {
        if (!atTarget && !mustWaitForClear)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            atTarget = true;
            InvokeRepeating("Attack", 0f, attackRate);
        }
        else if (Vector3.Distance(other.transform.position, transform.position)
            < moveSpeed)
        {
            mustWaitForClear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            atTarget = false;
        }
        else
        {
            mustWaitForClear = false;
        }
    }

    void Attack()
    {
        SurvivalHealthManager.SignalDamage(strength);
    }

    public static void DisableAttack()
    {
        if (OnStopAttack != null)
        {
            OnStopAttack();
        }
    }

    void StopAttack()
    {
        CancelInvoke("Attack");
    }
}
