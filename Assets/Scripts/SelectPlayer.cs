using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayer : MonoBehaviour {
	public Dropdown dd;
	public Button nextButton;
	// Use this for initialization
	void Start() {
		dd.ClearOptions();
		List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
		data.Add(new Dropdown.OptionData("Please Select a Value Below"));
		foreach (string user in ProfileManager.getUsers()) {
			data.Add(new Dropdown.OptionData(user));
		}
		dd.AddOptions(data);
		//Select the current player
		int value = PlayerPrefs.GetInt(GameControl.PLAYER_NUMBER, 0) + 1;
		if (value > ProfileManager.getAmountOfUsers()) {
			value = 0;
		}
		dd.value = value;
	}

	// Update is called once per frame
	void Update() {
		nextButton.enabled = dd.value > 0;
	}

	public void myDropdownValueChangedHandler() {
		Debug.Log("Selected: " + dd.value);
		if (dd.value > 0) {
			//Load profile
			PlayerPrefs.SetInt(GameControl.PLAYER_NUMBER,
			dd.value - 1);
		}
	}

	public void next() {
		GameControl.LoadLevel("Main");
	}
}
