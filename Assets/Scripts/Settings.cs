using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Settings : MonoBehaviour
{
	public Toggle ones, zeroes;
	// Use this for initialization
	void Awake ()
	{
		ones.isOn = PlayerPrefs.GetInt ("ones") == 1;
		zeroes.isOn = PlayerPrefs.GetInt ("zeroes") == 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void back ()
	{
		GameControl.LoadLevel ("Main");
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
}
