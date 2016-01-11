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

	public void donate(){
		Application.OpenURL("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=5CYSFRLWSCD76&lc=US&item_name=Support%20Development&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted");
	}
}
