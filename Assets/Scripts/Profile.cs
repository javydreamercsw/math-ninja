using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour {
	public Text playerName;
	public Image mult, subs;

	// Use this for initialization
	void Awake() {
		playerName.text = "Name: " + ProfileManager.getStringSetting(GameControl.NAME);
	}

	// Update is called once per frame
	void Update() {
		if (GameControl.instance != null) {
			mult.sprite = GameControl.instance.getLevelSprite(Game.MODE.MULTIPLICATION);
			subs.sprite = GameControl.instance.getLevelSprite(Game.MODE.SUBSTRACTION);
		}
	}

	public void changeProfile() {
		GameControl.LoadLevel("Select Player");
	}

	public void createProfile() {
		GameControl.LoadLevel("New Player");
	}

	public void exit() {
		PlayerPrefs.Save();
		GameControl.LoadLevel("Menu");
	}

	public void reset() {
		ProfileManager.resetProfile();
		playerName.text = "";
	}
}
