using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class NewPlayer : MonoBehaviour {
	public InputField input;

	// Update is called once per frame
	void Update () {
		PlayerPrefs.SetString (GameControl.NAME, input.text);
	}

	public void next(){
		PlayerPrefs.SetInt (GameControl.LEVEL, 0);
		GameControl.LoadLevel ("Welcome");
	}
}
