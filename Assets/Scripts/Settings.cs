using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
	public Toggle ones, zeroes, challenge;
	public Dropdown level, mode;
	private bool active = false;
	// Use this for initialization
	void Awake() {
		ones.isOn = ProfileManager.getIntSetting("ones", 0) == 1;
		zeroes.isOn = ProfileManager.getIntSetting("zeroes", 0) == 1;
		challenge.isOn = ProfileManager.getIntSetting("challenge", 0) == 1;
		int l = ProfileManager.getIntSetting(GameControl.LEVEL +
			ProfileManager.getStringSetting(GameControl.MODE)) - 1;
		level.value = l > 0 && l < 8 ? l : 1;
		level.gameObject.SetActive(false);
		//Load modes
		mode.ClearOptions();
		List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
		int index = 0;
		int i = 0;
		String currentMode = ProfileManager.getStringSetting(GameControl.MODE);
		foreach (Game.MODE m in Enum.GetValues(typeof(Game.MODE))) {
			data.Add(new Dropdown.OptionData(m.ToString()));
			if (m.ToString().Equals(currentMode)) {
				index = i;
			}
			i++;
		}
		mode.AddOptions(data);
		//Preselect current mode
		mode.value = index;
	}

	public void myDropdownValueChangedHandler() {
		Debug.Log("Selected: " + mode.value);
		//Save Mode
		ProfileManager.setStringSetting(GameControl.MODE,
		mode.options[mode.value].text);
	}


	public void back() {
		GameControl.LoadLevel("Menu");
		PlayerPrefs.Save();
	}

	public void togleOnes() {
		ProfileManager.setIntSetting("ones", ones.isOn ? 1 : 0);
		PlayerPrefs.Save();
	}

	public void toggleZeroes() {
		ProfileManager.setIntSetting("zeroes", zeroes.isOn ? 1 : 0);
		PlayerPrefs.Save();
	}

	public void toggleChallenge() {
		ProfileManager.setIntSetting("challenge", challenge.isOn ? 1 : 0);
		PlayerPrefs.Save();
	}

	public void updateLevel() {
		ProfileManager.setIntSetting(GameControl.LEVEL +
			ProfileManager.getStringSetting(GameControl.MODE), level.value + 1);
	}

	public void toggle() {
		active = !active;
		level.gameObject.SetActive(active);
	}
}
