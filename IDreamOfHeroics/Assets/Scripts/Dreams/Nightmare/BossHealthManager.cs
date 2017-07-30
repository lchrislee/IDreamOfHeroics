using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthManager : MonoBehaviour {

    public Text healthText;

    public BossData boss;
    int currentHealth;

    public delegate void TakeDamageHandler(int damage);
    public static event TakeDamageHandler OnTakeDamage;

    void Awake()
    {
        OnTakeDamage += TakeDamage;
    }

	// Use this for initialization
	void Start () {
        healthText = GetComponent<Text>();
	}
	
    public void InitializeAndStart(BossData bossData)
    {
        boss = bossData;
        currentHealth = boss.maxHealth;
    }

	// Update is called once per frame
	void Update () {
        updateHealth();
	}

    public static void Damage(int amount)
    {
        if (OnTakeDamage != null)
        {
            OnTakeDamage(amount);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage - boss.defense;
    }

    void updateHealth()
    {
        healthText.text = "Boss: " + currentHealth.ToString() 
            + " / " + boss.maxHealth.ToString();
    }
}
