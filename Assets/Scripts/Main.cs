using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
	public void load ()
	{
		Debug.Log ("Player: " + PlayerPrefs.GetString (GameControl.NAME));
		if (PlayerPrefs.GetString (GameControl.NAME) == "") {
			GameControl.LoadLevel ("New Player");
		} else {
			GameControl.LoadLevel ("Welcome");
		}
	}

	public void ShowMenu ()
	{
		GameControl.LoadLevel ("Menu");
	}
}
