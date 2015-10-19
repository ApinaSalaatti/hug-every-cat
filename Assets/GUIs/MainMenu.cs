using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
	[SerializeField]
	private GameObject continueButton;
	[SerializeField]
	private Text saveInfoText;
	[SerializeField]
	private GameObject noCatsDialog;

	// Use this for initialization
	void Start () {
		/*
		if(GameSaveLoad.Instance.SavePresent) {
			continueButton.SetActive(true);
			SaveInfo info = GameSaveLoad.Instance.GetSaveInfo();
			saveInfoText.text = "Saved: " + info.time;
		}*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenCatstagram() {
		Application.LoadLevel(4);
	}

	public void OpenCatCreator() {
		Application.LoadLevel(2);
	}

	public void ShowNoCatsDialog() {
		noCatsDialog.SetActive(true);
	}
	public void HideNoCatsDialog() {
		noCatsDialog.SetActive(false);
	}

	public void NewGame() {
		//WorldInit.CreateInitFile(WorldInitMode.NEW);
		//Application.LoadLevel(1);
	

		if(CatLoadDialog.Instance.CatsAvailable > 0) {
			CatLoadDialog.Instance.catSelectedListeners += OnCatLoad;
			CatLoadDialog.Instance.cancelListeners += OnCatLoadCancel;
			CatLoadDialog.Instance.ShowDialog();
		}
		else {
			ShowNoCatsDialog();
		}
	}

	private void OnCatLoadCancel() {
		CatLoadDialog.Instance.catSelectedListeners -= OnCatLoad;
		CatLoadDialog.Instance.cancelListeners -= OnCatLoadCancel;
	}
	private void OnCatLoad(string filename, CatExportImportData cat) {
		CatLoadDialog.Instance.catSelectedListeners -= OnCatLoad;
		CatLoadDialog.Instance.cancelListeners -= OnCatLoadCancel;

		CatExportImport.Instance.ExportCat(cat, "startingCat");

		if(GameSaveLoad.Instance.SavePresent) {
			// Game already played before, skip the intro
			Application.LoadLevel(7);
		}
		else {
			// Go to intro
			Application.LoadLevel(6);
		}
	}

	public void LoadGame() {
		//WorldInit.CreateInitFile(WorldInitMode.LOAD);
		//Application.LoadLevel(2);
	}

	public void QuitGame() {
		Application.Quit();
	}
}
