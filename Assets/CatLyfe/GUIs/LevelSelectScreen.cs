using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class NameToButton {
	public string name;
	public GameObject button;
}
public class LevelSelectScreen : MonoBehaviour {
	[SerializeField]
	private NameToButton[] buttons;

	// Use this for initialization
	void Start () {
		List<string> unlocked = LevelManager.Instance.Unlocked;
		for(int i = 0; i < unlocked.Count; i++) {
			string s = unlocked[i];
			EnableButton(s);
		}
	}

	private void EnableButton(string b) {
		Debug.Log(b);
		for(int i = 0; i < buttons.Length; i++) {
			if(buttons[i].name == b) {
				buttons[i].button.SetActive(true);
			}
		}
	}

	public void BackToMainMenu() {
		Application.LoadLevel(1);
	}
}
