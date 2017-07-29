using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealitySkillLevelManager : MonoBehaviour {

    public GameObject newSkillPanel;
    public GameObject playerSkillList;
    public Text takeSkillContinue;

    bool shouldShowNewSkills;
    int skillCount;
    SkillData[] skills;
    SkillData[] skillTier;
    int selectedNewSkill;

    void Awake()
    {
        shouldShowNewSkills = (PlayerPrefsManager.LoadLevelNumber() % 5) == 0;
        skillCount = PlayerPrefsManager.LoadSkillCount();
        if (skillCount > 0)
        {
            skills = PlayerPrefsManager.LoadPlayerSkills();
        }

        if (shouldShowNewSkills)
        {
            int skillTierValue = PlayerPrefsManager.LoadStageDifficulty();
            skillTier = ResourceManager.LoadSkillTier(skillTierValue);
        }
    }

	// Use this for initialization
	void Start () {
        newSkillPanel.SetActive(shouldShowNewSkills);
        playerSkillList.SetActive(skillCount > 0);

        if (shouldShowNewSkills)
        {
            takeSkillContinue.text = "Take Skill";
        }
        else
        {
            takeSkillContinue.text = "Continue";
        }
	}

    public void FinalizeLevel()
    {
        if (shouldShowNewSkills)
        {
            PlayerPrefsManager.SaveSkill(
                skillTier[selectedNewSkill], 
                skillCount
            );
        }
        PlaySessionManager.instance.ShowNextDream();
    }
}
