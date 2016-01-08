using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Profile : MonoBehaviour
{
	public InputField playerName;
	public Text level;
	public Image image;

	// Use this for initialization
	void Awake ()
	{
		playerName.text = PlayerPrefs.GetString (GameControl.NAME);
	}
	
	// Update is called once per frame
	void Update ()
	{
		PlayerPrefs.SetString (GameControl.NAME, playerName.text);
		level.text = "Level: " + PlayerPrefs.GetInt (GameControl.LEVEL);
		if(GameControl.instance!=null){
			Sprite s = GameControl.instance.getLevelSprite ();
			image.sprite = s;
		}
	}

	public void exit ()
	{
		PlayerPrefs.Save ();
		GameControl.LoadLevel ("Main");
	}

	public void reset ()
	{
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetInt (GameControl.LEVEL, 0);
		playerName.text = "";
	}
}
