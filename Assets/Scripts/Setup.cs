using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setup : MonoBehaviour {
	public Dropdown level, mode;
	public InputField input;

	// Use this for initialization
	void Awake() {
		ProfileManager.showProfile(PlayerPrefs.GetInt(GameControl.PLAYER_NUMBER, 0));
		mode.ClearOptions();
		List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
		foreach (Game.MODE m in Enum.GetValues(typeof(Game.MODE))) {
			if (!m.Equals(Game.MODE.ALL)) {
				data.Add(new Dropdown.OptionData(m.ToString()));
			}
		}
		mode.AddOptions(data);
		updateDisplay();
	}

	public void exit() {
		PlayerPrefs.Save();
		GameControl.LoadLevel("Menu");
	}

	public void updateDisplay() {
		Debug.Log("Updating level config: " + (level.value + 1) + ": " + mode.options[mode.value].text);
		input.text = ProfileManager.getStringSetting("level" + (level.value + 1)
			+ mode.options[mode.value].text);
	}

	public void save() {
		Debug.Log("Value changed from: "
			+ ProfileManager.getStringSetting("level" + (level.value + 1)) +
		" to: " + input.text);
		//Make sure values are valid
		if (isValid(input.text)) {
			ProfileManager.setStringSetting("level"
				+ (level.value + 1), input.text);
		}
		else {
			//TODO
			Debug.Log("Not valid string.");
		}
	}

	private bool isValid(string val) {
		if (val.Trim().Equals(string.Empty)) {
			Debug.Log("Invalid empty string!");
			return false;
		}
		else {
			string[] values = val.Split(',');
			if (values.Length < 3) {
				Debug.Log("Invalid value. Must have 3 fields or more!");
				return false;
			}
			int temp;
			if (Int32.TryParse(values[1], out temp)) {
				for (int i = 2; i < values.Length; i++) {
					try {
						//make sure they are real expressions.
						Game.Evaluate(values[i]);
					}
					catch (Exception e) {
						Debug.Log(e.ToString());
						return false;
					}
				}
			}
			else {
				Debug.Log("Invalid limit!");
				return false;
			}
		}
		return true;
	}
}
