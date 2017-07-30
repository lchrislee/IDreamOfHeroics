using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour {

    int maxHp = 20;
    int currentHp;
    int defense;

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

    public void InitializeStats(int hp, int def)
    {
        maxHp = hp;
        currentHp = maxHp;
        defense = def;
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
        currentHp -= damage - defense;
        if (currentHp <= 0 && isAlive)
        {
            currentHp = 0;
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
