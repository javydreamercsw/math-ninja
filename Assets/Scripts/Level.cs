using System;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour {

	public Text mess;
	public Image image;
	// Use this for initialization
	void Awake() {
		ProfileManager.setStringSetting(GameControl.MODE, Game.MODE.MULTIPLICATION.ToString());
		ProfileManager.setIntSetting(GameControl.LEVEL
			+ ProfileManager.getStringSetting(GameControl.MODE), 1);
		//------------------
		int level = ProfileManager.getIntSetting(GameControl.LEVEL
			+ ProfileManager.getStringSetting(GameControl.MODE));
		if (level > 0) {
			string[] levelInfo = ProfileManager.getStringSetting("level" + level +
				ProfileManager.getStringSetting(GameControl.MODE)).Split(',');
			mess.text = "Your current belt is " + levelInfo[0];
		}
		else {
			mess.text = "Your current belt is white";
		}
		Game.MODE mode = (Game.MODE)Enum.Parse(typeof(Game.MODE),
			ProfileManager.getStringSetting(GameControl.MODE));
		if (GameControl.instance != null) {
			Sprite s = GameControl.instance.getLevelSprite(mode);
			if (s != null) {
				image.sprite = s;
			}
		}
	}

	public void exit() {
		GameControl.LoadLevel("Main");
	}
}
