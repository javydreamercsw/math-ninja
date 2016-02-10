using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	public Text text, time, left;
	public InputField answer;
	private string color;
	private int limit, maxFailures = 3;
	private List<Entry> operations = new List<Entry> ();
	private List<int> used = new List<int> ();
	private Entry currentOp;
	private string INVALID = "Invalid answer, make sure answer is a valid number!";
	private float currentTime = 0.0f, executedTime = 0.0f, timeToWait = 3.0f, finalTimer = 60.0f * 4;
	private bool displayMessage = false, timer = false, exit = false;
	private string message;
	private int failures = 0, finalTestAmount = 82;
	private AudioSource source;
	public AudioClip lose1, lose2, timeover;
	private int level, answers, questionsLeft = 0;

	// Use this for initialization
	void Awake ()
	{
		source = GetComponent<AudioSource> ();
		LoadLevel ();
	}

	private bool containsOperation (string op)
	{
		foreach (Entry temp in operations) {
			if (temp.getOperation ().Equals (op)) {
				//Is the same, no need to add.
				return true;
			}
		}
		return false;
	}

	private void addOperation (Entry e)
	{
		//Check if operation is already in
		if (!shouldIgnore (e.getOperation ()) && !containsOperation (e.getOperation ())) {
			//Look closer
			//This assumes simple operations with to operands and one operation. 
			//It might not work for complex operations.
			string op = e.getOperation ();
			string left = "", right = "", sign = "";
			if (op.Contains ("x")) {
				sign = "x";
				left = op.Substring (0, op.IndexOf ("x"));
				right = op.Substring (op.IndexOf ("x") + 1);
			}
			if (!containsOperation (left + sign + right) && !containsOperation (right + sign + left)) {
				Debug.Log ("Adding: " + e.getOperation ());
				operations.Add (e);
			} else {
				Debug.Log ("Ignoring duplicated operation: " + e.getOperation ());
			}
		}
	}

	private Boolean shouldIgnore (string op)
	{
		int one = PlayerPrefs.GetInt ("ones", 1);
		int zero = PlayerPrefs.GetInt ("zeroes", 1);
		if (one == 0 && (op.StartsWith ("1x") || op.EndsWith ("x1"))) {
			return true;
		}
		if (zero == 0 && (op.StartsWith ("0x") || op.EndsWith ("x0"))) {
			return true;
		}
		return false;
	}

	private void LoadLevel ()
	{
		used.Clear ();
		answers = 0;
		operations.Clear ();
		//Load level
		level = PlayerPrefs.GetInt (GameControl.LEVEL, 0);
		for (int x = 1; x <= level + 1; x++) {
			//Get the level file and get ready
			string[] levelInfo = PlayerPrefs.GetString ("level" + (level + 1)).Split (',');
			if (levelInfo != null && levelInfo.Length > 2) {
				//Valid level (color, limit and at least one operation)
				color = levelInfo [0];
				if (Int32.TryParse (levelInfo [1], out limit)) {
					//Parse operations
					for (int i = 2; i < levelInfo.Length; i++) {
						//Debug.Log (levelInfo [i]);
						if (levelInfo [i].Contains ("*")) {
							//We need to expand this.
							for (int j = 0; j < limit; j++) {
								addOperation (new Entry (levelInfo [i].Replace ("*", "" + j)));
							}
						} else {
							addOperation (new Entry (levelInfo [i]));
						}
					}
					questionsLeft = operations.Count;
					if (questionsLeft > 0) {
						//Load the first question
						nextOp ();
					}
				} else {
					Debug.Log ("Invalid level limit: " + levelInfo [1]);
				}
			} else {
				Debug.Log ("Empty file???");
			}
			if (level == 6) {
				Debug.Log ("Level 7!");
				//This is the final level. 82 random operations from all levels with a 4 minute time limit.
				timer = true;
				executedTime = Time.time;
				timeToWait = 1.0f;
			}
		}
	}

	private void nextOp ()
	{
		if ((used.Count == operations.Count) || (timer && answers == finalTestAmount)) {
			display ("Congratulations!\nYou are now a " + color + " belt!");
			PlayerPrefs.SetInt (GameControl.LEVEL, PlayerPrefs.GetInt (GameControl.LEVEL) + 1);
			PlayerPrefs.Save ();
			timer = false;
			GameControl.LoadLevel ("Level");
		} else {
			Boolean valid = false;
			int index = -1;
			while (!valid) {
				index = UnityEngine.Random.Range (0, operations.Count);
				valid = !used.Contains (index);
			}
			used.Add (index);
			currentOp = operations [index];
			Debug.Log ("Current op: " + currentOp.getOperation ());
		}
	}

	public void Answer ()
	{
		double result = 0;
		if (Double.TryParse (answer.text, out result)) {
			//Valid result
			//Parse operation
			double temp = Evaluate (currentOp.getOperation ());
			currentOp.setUsed (true);
			Debug.Log ("Comparing answer: " + result + " with " + temp);
			if (result == temp) {
				display ("Correct!\nThe answer is: " + temp + "!");
				currentOp.setResult (true);
				answers++;
				questionsLeft -= 1;
				failures = 0;
				nextOp ();
			} else {
				failures++;
				currentOp.setResult (false);
				if (level == 7) {
					nextOp ();
				} else {
					if (failures == maxFailures) {
						display ("Incorrect! The answer is: " + temp + ".\nTry again!");
						playLoseSound ();
					} else {
						display ("Incorrect!\nTry again!");
						playLoseSound ();
					}
				}
			}
		} else {
			display (INVALID);
		}
		answer.text = "";
	}

	private void display (string mess)
	{
		message = mess;
		displayMessage = true;
		executedTime = Time.time;
	}

	public static double Evaluate (String expr)
	{

		Stack<String> stack = new Stack<String> ();

		string value = "";
		for (int i = 0; i < expr.Length; i++) {
			String s = expr.Substring (i, 1);
			char chr = s.ToCharArray () [0];

			if (!char.IsDigit (chr) && chr != '.' && value != "") {
				stack.Push (value);
				value = "";
			}

			if (s.Equals ("(")) {

				string innerExp = "";
				i++; //Fetch Next Character
				int bracketCount = 0;
				for (; i < expr.Length; i++) {
					s = expr.Substring (i, 1);

					if (s.Equals ("(")) {
						bracketCount++;
					}

					if (s.Equals (")")) {
						if (bracketCount == 0) {
							break;
						} else {
							bracketCount--;
						}
					}
					innerExp += s;
				}

				stack.Push (Evaluate (innerExp).ToString ());

			} else if (s.Equals ("+"))
				stack.Push (s);
			else if (s.Equals ("-"))
				stack.Push (s);
			else if (s.Equals ("x"))
				stack.Push (s);
			else if (s.Equals ("/"))
				stack.Push (s);
			else if (s.Equals ("sqrt"))
				stack.Push (s);
			else if (s.Equals (")")) {
			} else if (char.IsDigit (chr) || chr == '.') {
				value += s;

				if (value.Split ('.').Length > 2)
					throw new Exception ("Invalid decimal.");

				if (i == (expr.Length - 1))
					stack.Push (value);

			} else {
				throw new Exception ("Invalid character.");
			}
		}


		double result = 0;
		while (stack.Count >= 3) {

			double right = Convert.ToDouble (stack.Pop ());
			string op = stack.Pop ();
			double left = Convert.ToDouble (stack.Pop ());

			if (op == "+") {
				result = left + right;
			} else if (op == "+") {
				result = left + right;
			} else if (op == "-") {
				result = left - right;
			} else if (op == "x") {
				result = left * right;
			} else if (op == "/") {
				result = left / right;
			}

			stack.Push (result.ToString ());
		}
			
		return Convert.ToDouble (stack.Pop ());
	}

	// Update is called once per frame
	void Update ()
	{
		if (!exit) {
			currentTime = Time.time;
			if (displayMessage) {
				text.text = message;
			} else {
				if (currentOp == null) {
					text.text = "Error Loading Level: " + level;
				} else {
					text.text = currentOp.getOperation ();
				}
			}
			if (executedTime != 0.0f) {
				if (currentTime - executedTime > timeToWait) {
					executedTime = 0.0f;
					displayMessage = false;
					if (failures == 3) {
						GameControl.LoadLevel ("Main");
					}
				}
			}
			if (timer) {
				//Display the timer
				float t = (executedTime + finalTimer) - currentTime;
				int minutes = Mathf.FloorToInt (t / 60F);
				int seconds = Mathf.FloorToInt (t - minutes * 60);
				time.text = string.Format ("{0:0}:{1:00}", minutes, seconds);
				questionsLeft = finalTestAmount - answers;
				//Check time left
				if (minutes == 0 && seconds == 0) {
					//Time is up!
					playSound (timeover);
					checkResults ();
				}
				//Check answers left
				if (questionsLeft == 0) {
					//I'm done. Stop timer and check answers
					checkResults ();
					timer = false;
				}
			} else {
				time.text = "";
			}
			left.text = "Left: " + questionsLeft;
		}
	}

	private void checkResults ()
	{
		int errors = 0;
		foreach (Entry e in operations) {
			if (!e.getResult () && e.getUsed ()) {
				errors++;
			}
		}
		if (errors > 0) {
			if (errors == 0) {
				nextOp ();
			} else {
				display ("You had " + (errors) + " errors.\nKeep trying!");
				playLoseSound ();
				System.Threading.Thread.Sleep (5000);
				exit = true;
				GameControl.LoadLevel ("Main");
			}
		}
	}

	private class Entry
	{
		private string operation;
		private bool result = false, used = false;

		public Entry (string op)
		{
			operation = op;
		}

		public string getOperation ()
		{
			return operation;
		}

		public void setResult (bool r)
		{
			result = r;
		}

		public bool getResult ()
		{
			return result;
		}

		public void setUsed (bool u)
		{
			used = u;
		}

		public bool getUsed ()
		{
			return used;
		}
	}

	public void playLoseSound ()
	{
		switch (UnityEngine.Random.Range (1, 2)) {
		case 1:
			playSound (lose1);
			break;
		case 2:
			playSound (lose2);
			break;
		}
	}

	private void playSound (AudioClip clip)
	{
		if (source.isPlaying) {
			source.Stop ();
		}
		source.volume = 1.0f;
		source.PlayOneShot (clip);
	}

	public void Quit ()
	{
		GameControl.LoadLevel ("Main");
	}
}
