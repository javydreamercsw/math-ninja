using UnityEngine;
using UnityEngine.UI;

public class Welcome : MonoBehaviour {
	public Text text;
	// Use this for initialization
	void Awake() {
		text.text = "Welcome " + ProfileManager.getStringSetting(GameControl.NAME)
			+ "!\nAre you ready to resume your quest?";
	}

	public void next() {
		GameControl.LoadLevel("Game");
	}

	public void quit() {
		GameControl.QuitRequest();
	}
}
