using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

	public Text prompt;

	// Use this for initialization
	void Start () {
		StartCoroutine (ExtendPrompt ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator ExtendPrompt(){
		prompt.text += "again...";
		yield return new WaitForSeconds (2);
	}
}
