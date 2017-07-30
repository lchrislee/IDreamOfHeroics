using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealityClassLevelManager : MonoBehaviour {

    public Text description;
    BattleClassType typeSelected = BattleClassType.ROGUE;

    public void DetailClass(BattleClass classSelected)
    {
        description.text = classSelected.classDescription;
        typeSelected = classSelected.type;
    }

    public void CompleteLevel()
    {
        Debug.Log("saving selected type: " + typeSelected.ToString());
        PlayerPrefsManager.SavePlayerClass(typeSelected);
        PlaySessionManager.instance.ShowNextDream();
    }
}
