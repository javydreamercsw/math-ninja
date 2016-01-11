using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public void ShowProfile ()
	{
		GameControl.LoadLevel ("Profile");
	}

	public void ShowSettings ()
	{
		GameControl.LoadLevel ("Settings");
	}

	public void ShowSetup ()
	{
		GameControl.LoadLevel ("Setup");
	}

	public void Back(){
		GameControl.LoadLevel ("Main");
	}
}
