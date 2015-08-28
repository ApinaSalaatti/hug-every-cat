using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextEffect : MonoBehaviour {
	[SerializeField]
	private Text textToShow;

	private Color defaultColor = new Color(1f, 191f/255f, 58f/255f);

	private float timeLeft;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.y += 1f * Time.deltaTime;
		transform.position = pos;

		timeLeft -= Time.deltaTime;
		if(timeLeft < 1f) {
			Color c = textToShow.color;
			c.a = timeLeft;
			textToShow.color = c;
		}
		if(timeLeft <= 0f) {
			Destroy(gameObject);
		}
	}

	public void ShowText(string text, float time) {
		ShowText(text, time, defaultColor);
	}
	public void ShowText(string text, float time, Color textColor) {
		textToShow.text = text;
		timeLeft = time;
		textToShow.color = textColor;
	}
}
