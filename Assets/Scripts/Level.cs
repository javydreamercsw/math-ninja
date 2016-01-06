using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Level : MonoBehaviour
{

	public Text mess;
	public Image image;
	// Use this for initialization
	void Awake ()
	{
		int level = PlayerPrefs.GetInt (GameControl.LEVEL);
		Sprite s= GameControl.instance.getLevelSprite ();
		if (s != null) {
			if (level > 0) {
				string[] levelInfo = GameControl.ReadFile ("level" + level);
				mess.text = "Your current belt is " + levelInfo [0];
			} else {
				mess.text = "Your current belt is white";
			}
			image.sprite = s;
		}
	}

	public void exit(){
		GameControl.LoadLevel ("Main");
	}
}
