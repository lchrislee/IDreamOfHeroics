using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSkillUIManager : MonoBehaviour {

    public delegate void UseSkillHandler(int skillNumber);
    public static event UseSkillHandler OnUseSkill;

    Skill[] skills;

    void Awake()
    {
        OnUseSkill += UseSkill;
    }

	// Use this for initialization
	void Start () {
        
	}
        
    public void InitializeAndEnable(Skill[] skillsToUse)
    {
        skills = skillsToUse;
        for (int i = skillsToUse.Length; i < 3; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public static void ActivateSkill(int skillNumber){
        if (OnUseSkill != null)
        {
            OnUseSkill(skillNumber);
        }
    }

    public void UseSkill(int skillNumber)
    {
        Debug.Log("Use skill " + skillNumber);
    }
}
