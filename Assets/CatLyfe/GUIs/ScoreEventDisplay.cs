using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreEventDisplay : MonoBehaviour {
	private Text text;

	private float sinceLastScore = 0f;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		sinceLastScore += Time.deltaTime;
		if(sinceLastScore > 2f && sinceLastScore < 3.1f) {
			float a = 1f - (sinceLastScore - 2f);
			Color col = text.color;
			col.a = a;
			text.color = col;
		}
	}

	public void ShowEvent(string eventName, string[] bonuses, int eventScore, int totalScore, int multiplier) {
		string s = eventName + " " + totalScore.ToString() + "pts (";

		s += eventScore + "pts";

		for(int i = 0; i < bonuses.Length; i++) {
			s += " + " + bonuses[i];
		}

		s += " x" + multiplier.ToString();

		s += ")";

		text.text = s;

		sinceLastScore = 0f;
		Color col = text.color;
		col.a = 1f;
		text.color = col;
	}
}
