using System;
using UnityEngine;

public static class ProfileManager {
	public static String NUMBER_OF_USERS = "NumberOfUsers", USER = "User";

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
		Debug.Log("Added user: " + username);
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
