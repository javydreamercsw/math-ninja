using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Settings : MonoBehaviour
{
	public Toggle ones, zeroes, challenge;
	public Dropdown level;
	private bool active = false;
	// Use this for initialization
	void Awake ()
	{
		ones.isOn = PlayerPrefs.GetInt ("ones", 0) == 1;
		zeroes.isOn = PlayerPrefs.GetInt ("zeroes", 0) == 1;
		challenge.isOn = PlayerPrefs.GetInt ("challenge", 0) == 1;
		int l = PlayerPrefs.GetInt (GameControl.LEVEL) - 1;
		level.value = l > 0 && l < 8 ? l : 1;
		level.gameObject.SetActive (false);
	}

	public void back ()
	{
		GameControl.LoadLevel ("Menu");
		PlayerPrefs.Save ();
	}

	public void togleOnes ()
	{
		PlayerPrefs.SetInt ("ones", ones.isOn ? 1 : 0);
		Debug.Log ("one=" + PlayerPrefs.GetInt ("ones"));
		PlayerPrefs.Save ();
	}

	public void toggleZeroes ()
	{
		PlayerPrefs.SetInt ("zeroes", zeroes.isOn ? 1 : 0);
		Debug.Log ("zero=" + PlayerPrefs.GetInt ("zeroes"));
		PlayerPrefs.Save ();
	}

	public void toggleChallenge ()
	{
		PlayerPrefs.SetInt ("challenge", challenge.isOn ? 1 : 0);
		Debug.Log ("challenge=" + PlayerPrefs.GetInt ("challenge"));
		PlayerPrefs.Save ();
	}

	public void updateLevel ()
	{
		PlayerPrefs.SetInt (GameControl.LEVEL, level.value + 1);
	}

	public void toggle ()
	{
		active = !active;
		if (active) {
			Debug.Log ("Show");
		} else {
			Debug.Log ("Hide");
		}
		level.gameObject.SetActive (active);
	}
}
