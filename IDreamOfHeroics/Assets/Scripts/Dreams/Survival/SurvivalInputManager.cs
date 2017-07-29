using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalInputManager : MonoBehaviour {

    public Transform crosshair;

    SkillData[] skills;
    float[] cooldownCount;
    BattleClass playerClass;
    bool canPrimary = true;
    bool canSecondary = true;

    public void InitializeAndMonitor(
        BattleClass classToUse, 
        SkillData[] skillsToUse
    )
    {
        playerClass = classToUse;
        skills = skillsToUse;
        cooldownCount = new float[skills.Length];
        for (int i = 0; i < cooldownCount.Length; ++i)
        {
            cooldownCount[i] = 0f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Right Click.
        if (playerClass.canMelee
            && Input.GetMouseButton(1)
            && canSecondary
        )
        {
            PerformMelee();
        }

        // Left Click.
        if (playerClass.canRange
            && Input.GetMouseButton(0)
            && canPrimary
        )
        {
            PerformRange();
        }

        // Skill 1
        if (skills.Length >= 1
            && false
            && Mathf.Approximately(cooldownCount[0], 0f)
        )
        {
            BattleSkillUIManager.ActivateSkill(0);
        }
        // Skill 2
        if (skills.Length >= 2
            && false
            && Mathf.Approximately(cooldownCount[1], 0f)
        )
        {
            BattleSkillUIManager.ActivateSkill(1);
        }

        // Skill 3
        if (skills.Length == 3
            && false
            && Mathf.Approximately(cooldownCount[2], 0f)
        )
        {
            BattleSkillUIManager.ActivateSkill(2);
        }
	}

    void PerformRange()
    {
        canPrimary = false;
        Debug.Log("Range Attack");
        Invoke("ResetRange", playerClass.attackCooldown);
    }

    void PerformMelee()
    {
        canSecondary = false;
        Debug.Log("Physical Attack");
        Invoke("ResetMelee", playerClass.attackCooldown);
    }

    void ResetRange()
    {
        canPrimary = true;
    }

    void ResetMelee()
    {
        canSecondary = true;
    }
}
