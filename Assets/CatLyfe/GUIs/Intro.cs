using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Intro : MonoBehaviour {
	[SerializeField]
	private Text[] textObjects;

	[SerializeField]
	private Text escToSkipText;

	private IntroText[] textInfos;
	private string[] storyTexts;

	private int currentText = 0;

	private float typeTimer = 0f;

	private bool introDone = false;

	// Use this for initialization
	void Start () {
		storyTexts = new string[textObjects.Length];
		textInfos = new IntroText[textObjects.Length];
		for(int i = 0; i < textObjects.Length; i++) {
			storyTexts[i] = textObjects[i].text;
			textInfos[i] =  textObjects[i].GetComponent<IntroText>();
			textObjects[i].text = "";
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!introDone) {
			typeTimer += Time.deltaTime;
			float time = textInfos[currentText].TypeTime;
			if(typeTimer >= time) {
				typeTimer = 0f;
				TypeText();
			}
		}

		if(Input.GetButtonDown("Cancel")) {
			if(!introDone) {
				SkipToEnd();
			}
			else {
				CloseIntro();
			}
		}
	}

	private void TypeText() {
		string wholeText = storyTexts[currentText];
		Text textObj = textObjects[currentText];

		float time = textInfos[currentText].TypeTime;
		if(time <= 0f) {
			textObj.text = wholeText;
		}
		else {
			textObj.text = wholeText.Substring(0, textObj.text.Length+1);
		}
		if(textObj.text == wholeText) {
			currentText++;
			typeTimer = -1f; // A short pause between sentences.
			if(currentText == storyTexts.Length) {
				introDone = true;
				escToSkipText.text = "Press ESC to proceed...";
			}
		}
	}

	private void SkipToEnd() {
		introDone = true;
		escToSkipText.text = "Press ESC to proceed...";
		for(int i = 0; i < textObjects.Length; i++) {
			textObjects[i].text = storyTexts[i];
		}
	}

	private void CloseIntro() {
		Application.LoadLevel(6); // Go to level select screen
	}
}
