using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * A dialog window that lists all the files in the given directory
 */
public class FileLoadDialog : MonoBehaviour {
	/*
	 * Global dialog stuff
	 */
	private static FileLoadDialog instance;

	public delegate void FileSelectedListener(string file);
	public delegate void CancelListener();

	/*
	 * Used by the instance
	 */
	[SerializeField]
	private GameObject dialogObject;
	[SerializeField]
	private GameObject fileSelectButtonPrefab;
	[SerializeField]
	private RectTransform fileSelectButtonParent;
	[SerializeField]
	private Button okButton;
	[SerializeField]
	private Button cancelButton;

	public FileSelectedListener fileSelectedListeners;
	public CancelListener cancelListeners;

	private Button[] fileSelectButtons;

	private string selectedFile;

	public static FileLoadDialog Instance() {
		if(!instance) {
			instance = FindObjectOfType<FileLoadDialog>();
			if(!instance) {
				Debug.LogError("There's no FileLoadDialog to be found you TURD!");
			}
		}
		return instance;
	}

	void Start() {
		okButton.onClick.AddListener(OkPressed);
		cancelButton.onClick.AddListener(CancelPressed);
	}

	private void FileSelected(string f) {
		selectedFile = f;
		Debug.Log(selectedFile);
	}

	private void OkPressed() {
		if(fileSelectedListeners != null) fileSelectedListeners(selectedFile);
		HideDialog();
	}
	private void CancelPressed() {
		if(cancelListeners != null) cancelListeners();
		HideDialog();
	}

	public void ShowDialog(string pathToDirectory) {
		// Clear old stuff
		if(fileSelectButtons != null && fileSelectButtons.Length > 0) {
			foreach(Button b in fileSelectButtons) {
				Destroy(b.gameObject);
			}
		}
		dialogObject.SetActive(true);

		string[] files = System.IO.Directory.GetFiles(pathToDirectory);
		fileSelectButtons = new Button[files.Length];
		for(int i = 0; i < files.Length; i++) {
			string f = files[i];
			GameObject b = Instantiate(fileSelectButtonPrefab) as GameObject;

			RectTransform rt = b.GetComponent<RectTransform>();
			rt.SetParent(fileSelectButtonParent);

			Text t = b.GetComponentInChildren<Text>();
			t.text = f;

			Button button = b.GetComponent<Button>();
			button.onClick.AddListener(() => FileSelected(f));

			fileSelectButtons[i] = button;
		}
	}
	public void HideDialog() {
		dialogObject.SetActive(false);
	}
}
