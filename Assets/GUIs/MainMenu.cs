using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
	[SerializeField]
	private GameObject continueButton;
	[SerializeField]
	private Text saveInfoText;

	// Use this for initialization
	void Start () {
		if(GameSaveLoad.Instance.SavePresent) {
			continueButton.SetActive(true);
			SaveInfo info = GameSaveLoad.Instance.GetSaveInfo();
			saveInfoText.text = "Saved: " + info.time;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewGame() {
		WorldInit.CreateInitFile(WorldInitMode.NEW);
		Application.LoadLevel(1);
	}

	public void LoadGame() {
		WorldInit.CreateInitFile(WorldInitMode.LOAD);
		Application.LoadLevel(2);
	}

	public void QuitGame() {
		Application.Quit();
	}
}
