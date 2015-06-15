using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatLoadDialog : MonoBehaviour {
	/*
	 * Global dialog stuff
	 */
	private static CatLoadDialog instance;
	
	public delegate void CatSelectedListener(string selectedCatFilename, GameObject cat);
	public delegate void CancelListener();

	public static CatLoadDialog Instance {
		get { return instance; }
	}

	void Awake() {
		instance = this;
	}

	/*
	 * Used by the instance
	 */
	[SerializeField]
	private GameObject dialogObject;
	[SerializeField]
	private GameObject catSelectButtonPrefab;
	[SerializeField]
	private RectTransform catSelectButtonParent;
	[SerializeField]
	private Button okButton;
	[SerializeField]
	private Button cancelButton;
	[SerializeField]
	private Text selectedCatInfo;
	
	public CatSelectedListener catSelectedListeners;
	public CancelListener cancelListeners;
	
	private Button[] catSelectButtons;
	private GameObject[] selectableCats;

	private string selectedCatFilename;
	private GameObject selectedCat;
	
	void Start() {
		okButton.onClick.AddListener(OkPressed);
		cancelButton.onClick.AddListener(CancelPressed);
	}
	
	private void CatSelected(string file, GameObject cat) {
		selectedCatFilename = file;
		selectedCat = cat;
		CatStats stats = cat.GetComponent<CatStats>();
		selectedCatInfo.text = stats.Name;
	}
	
	private void OkPressed() {
		if(catSelectedListeners != null) catSelectedListeners(selectedCatFilename, selectedCat);
		HideDialog();
		DestroySelectables();
	}
	private void CancelPressed() {
		if(cancelListeners != null) cancelListeners();
		HideDialog();
		DestroySelectables();
	}

	private void DestroySelectables() {
		foreach(GameObject g in selectableCats) {
			Destroy(g);
		}
	}
	
	public void ShowDialog(string pathToDirectory) {
		// Clear old stuff
		if(catSelectButtons != null && catSelectButtons.Length > 0) {
			foreach(Button b in catSelectButtons) {
				Destroy(b.gameObject);
			}
		}
		dialogObject.SetActive(true);
		
		string[] files = System.IO.Directory.GetFiles(pathToDirectory, "*.catFile");
		catSelectButtons = new Button[files.Length];
		selectableCats = new GameObject[files.Length];
		for(int i = 0; i < files.Length; i++) {
			string f = files[i];
			string filename = System.IO.Path.GetFileName(f);

			GameObject b = Instantiate(catSelectButtonPrefab) as GameObject;
			
			RectTransform rt = b.GetComponent<RectTransform>();
			rt.SetParent(catSelectButtonParent);
			
			Text t = b.GetComponentInChildren<Text>();
			t.text = f;

			GameObject cat = CatExportImport.Instance.ImportCat(filename);
			selectableCats[i] = cat;
			
			Button button = b.GetComponent<Button>();
			button.onClick.AddListener(() => CatSelected(filename, cat));
			
			catSelectButtons[i] = button;
		}
	}
	public void HideDialog() {
		dialogObject.SetActive(false);
	}
}
