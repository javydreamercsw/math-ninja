using UnityEngine;
//using UnityEngine.Advertisements;

public class Main : MonoBehaviour {

	//public void ShowAd() {
	//	if (Advertisement.IsReady()) {
	//		Advertisement.Show();
	//	}
	//}

	public void load() {
		Debug.Log("Player: " + PlayerPrefs.GetString(GameControl.NAME));
		if (PlayerPrefs.GetInt(ProfileManager.NUMBER_OF_USERS) == 0) {
			//No players detected Create one
			GameControl.LoadLevel("New Player");
		}
		else if (ProfileManager.getUsers().Length > 1) {
			//Need to select a player
			GameControl.LoadLevel("Select Player");
		}
		else {
			GameControl.LoadLevel("Welcome");
		}
	}

	public void ShowMenu() {
		GameControl.LoadLevel("Menu");
	}
}
