using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Setup : MonoBehaviour
{
	public Dropdown level;
	public InputField input;

	// Use this for initialization
	void Awake ()
	{
		updateDisplay ();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void exit ()
	{
		PlayerPrefs.Save ();
		GameControl.LoadLevel ("Menu");
	}

	public void updateDisplay ()
	{
		input.text = PlayerPrefs.GetString ("level" + (level.value + 1));
	}

	public void save ()
	{
		Debug.Log ("Value changed from: " + PlayerPrefs.GetString ("level" + (level.value + 1)) +
		" to: " + input.text);
		//Make sure values are valid
		if (isValid (input.text)) {
			PlayerPrefs.SetString ("level" + (level.value + 1), input.text);
		}else{
			//TODO
			Debug.Log("Not valid string.");
		}
	}

	private bool isValid (string val)
	{
		if (val.Trim ().Equals (string.Empty)) {
			Debug.Log ("Invalid empty string!");
			return false;
		} else {
			string[] values = val.Split (',');
			if (values.Length < 3) {
				Debug.Log ("Invalid value. Must have 3 fields or more!");
				return false;
			}
			int temp;
			if (Int32.TryParse (values [1], out temp)) {
				for (int i = 2; i < values.Length; i++) {
					try {
					//make sure they are real expressions.
						Game.Evaluate (values [i]);
					} catch (Exception e) {
						return false;
					}
				}
			} else {
				Debug.Log ("Invalid limt!");
				return false;
			}
		}
		return true;
	}
}
