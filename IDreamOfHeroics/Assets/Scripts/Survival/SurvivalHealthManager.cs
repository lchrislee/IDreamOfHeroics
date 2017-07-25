using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalHealthManager : MonoBehaviour {

    int maxHp = 20;
    int currentHp;

    static bool isAlive = true;

    public delegate void SignalDamageHandler(int strength);
    public static event SignalDamageHandler OnTakeDamage;

    private Text health;

    void Awake()
    {
        health = GetComponentInParent<Text>();
    }

	// Use this for initialization
	void Start () {
        OnTakeDamage += TakeDamage;
	}

    void Update()
    {
        SetHealth();
    }

    public void InitializePlayerStats(int hp)
    {
        maxHp = hp;
        currentHp = maxHp;
        SetHealth();
    }

    public static void SignalDamage(int strength)
    {
        if (OnTakeDamage != null && isAlive)
        {
            OnTakeDamage(strength);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0 && isAlive)
        {
            isAlive = false;
            OnTakeDamage -= TakeDamage;
            SurvivalLevelManager.StopScene();
        }
    }

    void SetHealth()
    {
        float percentage = currentHp;
        percentage /= maxHp;
        if (percentage < 0.25f)
        {
            health.color = Color.red;
        }
        else
        {
            health.color = Color.white;
        }
        health.text = "Health: " + currentHp.ToString() + " / " + maxHp.ToString();
    }
}
