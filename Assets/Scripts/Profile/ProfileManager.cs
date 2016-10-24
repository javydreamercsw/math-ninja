using System;
using UnityEngine;

public static class ProfileManager {
	public static String NUMBER_OF_USERS = "NumberOfUsers", USER = "User";

	private static string[] multiplication = {
		"yellow,10,6x6,6x7,6x8,7x7,7x8,8x8,1x*,0x*",
		"green,10,10x*",
		"blue,10,3x6,3x7,3x8,4x6,4x7,4x8",
		"purple,11,11x*",
		"brown,10,2x*,3x3,3x4,4x4",
		"red,10,9x*",
		"black,10,5x*"
	};
	private static string[] substraction = {
		 "yellow,5,5-*",
		 "green,10,10-*",
		 "blue,20,20-*",
		 "purple,50,50-*",
		 "brown,75,75-*",
		 "red,90,90-*",
		 "black,100,100-*"
	};

	public static String[] getUsers() {
		if (getAmountOfUsers() > 0) {
			String[] users = new String[getAmountOfUsers()];
			for (int i = 0; i < users.Length; i++) {
				showProfile(i);
				String temp = PlayerPrefs.GetString(ProfileManager.USER
					+ i + GameControl.NAME, "");
				users[i] = temp;
				Debug.Log("Added player: " + temp + " to the list.");
			}
			return users;
		}
		else {
			return new String[] { };
		}
	}

	public static void addUser(String username) {
		//Update the amount
		PlayerPrefs.SetInt(NUMBER_OF_USERS, getAmountOfUsers() + 1);
		//Add to the list
		PlayerPrefs.SetString(USER + getAmountOfUsers(), username);
		foreach (Game.MODE m in Enum.GetValues(typeof(Game.MODE))) {
			if (!m.Equals(Game.MODE.ALL)) {
				ProfileManager.setIntSetting(GameControl.LEVEL + m.ToString(), 0);
			}
		}
		PlayerPrefs.SetInt(GameControl.PLAYER_NUMBER, getAmountOfUsers());
		//Load modes
		loadDefaultModes();
		Debug.Log("Added user: " + username + "(" + getAmountOfUsers() + ")");
	}

	static void loadDefaultModes() {
		loadMode(Game.MODE.MULTIPLICATION, multiplication);
		loadMode(Game.MODE.SUBSTRACTION, substraction);
	}

	static void loadMode(Game.MODE m, String[] values) {
		for (int i = 0; i < values.Length; i++) {
			if (PlayerPrefs.GetString(ProfileManager.USER
			+ PlayerPrefs.GetInt(GameControl.PLAYER_NUMBER, 0)
			+ "level" + (i + 1) + m.ToString()) == "") {
				PlayerPrefs.SetString(ProfileManager.USER
			+ PlayerPrefs.GetInt(GameControl.PLAYER_NUMBER, 0)
			+ "level" + (i + 1) + m.ToString(), values[i]);
				Debug.Log("Saving: " + ProfileManager.USER
			+ PlayerPrefs.GetInt(GameControl.PLAYER_NUMBER, 0)
			+ "level" + (i + 1) + m.ToString());
			}
		}
	}

	public static void showProfile(int i) {
		Debug.Log("Profile: " + i);
		Debug.Log("Name: " + PlayerPrefs.GetString(ProfileManager.USER
			+ i + GameControl.NAME));
		Debug.Log("Level: " + PlayerPrefs.GetInt(ProfileManager.USER
			+ i + GameControl.LEVEL));
		Debug.Log("Ones: " + PlayerPrefs.GetInt(ProfileManager.USER
			+ i + "ones"));
		Debug.Log("Zeroes: " + PlayerPrefs.GetInt(ProfileManager.USER
			+ i + "zeroes"));
		Debug.Log("Challenge: " + PlayerPrefs.GetInt(ProfileManager.USER
			+ i + "challenge"));
	}

	internal static void resetProfile() {
		foreach (Game.MODE m in Enum.GetValues(typeof(Game.MODE))) {
			if (!m.Equals(Game.MODE.ALL)) {
				ProfileManager.setIntSetting(GameControl.LEVEL + m.ToString(), 0);
			}
		}
		loadDefaultModes();
	}

	public static void resetAll() {
		PlayerPrefs.DeleteAll();
	}

	public static int getAmountOfUsers() {
		return PlayerPrefs.GetInt(NUMBER_OF_USERS, 0);
	}

	/// <summary>
	/// Gets the specified int setting for the current user
	/// </summary>
	/// <param name="name">Variable to retrieve.</param>
	/// <returns>The requested value or , 0 if not found.</returns>
	public static int getIntSetting(String name) {
		return getIntSetting(name, 0);
	}

	/// <summary>
	/// Gets the specified int setting for the current user
	/// </summary>
	/// <param name="name">Variable to retrieve.</param>
	/// <param name="defaultValue">if not found, the default value to return.</param>
	/// <returns>The requested value or , defaultValue if not found.</returns>
	public static int getIntSetting(String name, int defaultValue) {
		return PlayerPrefs.GetInt(ProfileManager.USER
			+ PlayerPrefs.GetInt(GameControl.PLAYER_NUMBER, 0) + name,
			defaultValue);
	}

	/// <summary>
	/// Sets the specified int setting for the current user
	/// </summary>
	/// <param name="name">Variable to set</param>
	/// <param name="value">Value to set</param>
	public static void setIntSetting(String name, int value) {
		PlayerPrefs.SetInt(ProfileManager.USER
			+ PlayerPrefs.GetInt(GameControl.PLAYER_NUMBER, 0) + name,
			value);
		PlayerPrefs.Save();
	}

	/// <summary>
	/// Gets the specified string setting for the current user
	/// </summary>
	/// <param name="name">Variable to retrieve.</param>
	/// <returns>The requested value or '' (empty string) if not found.</returns>
	public static String getStringSetting(String name) {
		return getStringSetting(name, "");
	}

	/// <summary>
	/// Gets the specified string setting for the current user
	/// </summary>
	/// <param name="name">Variable to retrieve.</param>
	/// <param name="defaultValue">if not found, the default value to return.</param>
	/// <returns>The requested value or defaultValue if not found.</returns>
	public static String getStringSetting(String name, String defaultValue) {
		return PlayerPrefs.GetString(ProfileManager.USER
			+ PlayerPrefs.GetInt(GameControl.PLAYER_NUMBER, 0) + name,
			defaultValue);
	}

	/// <summary>
	/// Sets the specified String setting for the current user
	/// </summary>
	/// <param name="name">Variable to set</param>
	/// <param name="value">Value to set</param>
	public static void setStringSetting(String name, String value) {
		PlayerPrefs.SetString(ProfileManager.USER
			+ PlayerPrefs.GetInt(GameControl.PLAYER_NUMBER, 0) + name,
			value);
		PlayerPrefs.Save();
	}
}
