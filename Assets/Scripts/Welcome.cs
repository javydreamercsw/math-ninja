using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Welcome : MonoBehaviour {
	public Text text;
	// Use this for initialization
	void Awake () {
		text.text = "Welcome "+ PlayerPrefs.GetString("Player_Name") +"!\nAre you ready to resume your quest?";
	}

	public void next(){
		GameControl.LoadLevel ("Game");
	}

	public void quit(){
		GameControl.QuitRequest ();
	}
}
