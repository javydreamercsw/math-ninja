using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.IO;

public class GameControl : MonoBehaviour
{
	public static GameControl instance;
	//TODO: Add folders for different types Sum, Division, etc. and prompt user for it.
	public static string DATA = "Assets\\Data\\Multiplication";
	public static string NAME = "Player_Name", LEVEL = "Level";
	public Sprite white, yellow, green, blue, purple, brown, red, black;

	public static void QuitRequest ()
	{
		Debug.Log ("I Want to quit!");
		Application.Quit ();
	}

	public void Quit ()
	{
		Debug.Log ("I Want to quit!");
		Application.Quit ();
	}

	void Awake ()
	{
		if (instance == null) {
			DontDestroyOnLoad (gameObject);
			instance = this;
		}
	}

	public static string[] ReadFile (string fileName)
	{
		Debug.Log ("Reading file: "+fileName);
		string[] result = null;
		TextAsset text = Resources.Load(fileName) as TextAsset;
		if (text != null) {
			result = text.text.Split (',');
		}
		Debug.Log("Done!");
		return result;
	}

	public static void LoadLevel (string level)
	{
		Debug.Log ("Loading level: " + level);
		//SceneManager.LoadScene (level);
		Application.LoadLevel (level);
	}

	public Sprite getLevelSprite ()
	{
		int level = PlayerPrefs.GetInt (GameControl.LEVEL);
		Sprite s = null;
		switch (level) {
		case 0:
			s = white;
			break;
		case 1:
			s = yellow;
			break;
		case 2:
			s = green;
			break;
		case 3:
			s = blue;
			break;
		case 4:
			s = purple;
			break;
		case 5:
			s = brown;
			break;
		case 6:
			s = red;
			break;
		case 7:
			s = black;
			break;
		default:
			Debug.Log ("Undefined level: " + level);
			break;
		}
		return s;
	}
}
