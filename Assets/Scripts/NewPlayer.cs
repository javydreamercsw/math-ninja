using UnityEngine;
using UnityEngine.UI;

public class NewPlayer : MonoBehaviour {
	public InputField input;

	public void next() {
		ProfileManager.addUser(input.text);
		PlayerPrefs.SetInt(GameControl.PLAYER_NUMBER,
			ProfileManager.getAmountOfUsers());
		ProfileManager.setStringSetting(GameControl.NAME, input.text);
		GameControl.LoadLevel("Welcome");
	}
}
