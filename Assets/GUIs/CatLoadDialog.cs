using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatLoadDialog : MonoBehaviour {
	/*
	 * Global dialog stuff
	 */
	private static CatLoadDialog instance;
	
	public delegate void CatSelectedListener(string selectedCatFilename, CatExportImportData cat);
	public delegate void CancelListener();
	
	public static CatLoadDialog Instance {
		get { return instance; }
	}

	private int catsAvailable;
	public int CatsAvailable {
		get { return catsAvailable; }
	}
	
	void Awake() {
		instance = this;

		string pathToDirectory = Globals.CatFolder;

		// Count available cats (is used in the main menu to determine if any cats have been created already)
		string[] files = System.IO.Directory.GetFiles(pathToDirectory, "*.catFile");
		catsAvailable = 0;
		for(int i = 0; i < files.Length; i++) {
			string filename = System.IO.Path.GetFileNameWithoutExtension(files[i]);
			// Don't count the starting cat
			if(!filename.Equals("startingCat"))
				catsAvailable++;
		}
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
	private CatExportImportData[] selectableCats;
	
	private string selectedCatFilename;
	private CatExportImportData selectedCat;
	
	void Start() {
		okButton.onClick.AddListener(OkPressed);
		cancelButton.onClick.AddListener(CancelPressed);
	}
	
	private void CatSelected(string file, CatExportImportData cat) {
		selectedCatFilename = file;
		selectedCat = cat;
		selectedCatInfo.text = cat.name;
	}
	
	private void OkPressed() {
		if(selectedCat != null) {
			if(catSelectedListeners != null) catSelectedListeners(selectedCatFilename, selectedCat);
			HideDialog();
			DestroySelectables();
		}
	}
	private void CancelPressed() {
		if(cancelListeners != null) cancelListeners();
		HideDialog();
		DestroySelectables();
	}
	
	private void DestroySelectables() {
		selectableCats = null;
		//foreach(GameObject g in selectableCats) {
		//	Destroy(g);
		//}
	}
	
	public void ShowDialog() {
		string pathToDirectory = Globals.CatFolder;

		// Clear old stuff
		if(catSelectButtons != null && catSelectButtons.Length > 0) {
			foreach(Button b in catSelectButtons) {
				if(b != null) Destroy(b.gameObject);
			}
		}
		dialogObject.SetActive(true);
		
		string[] files = System.IO.Directory.GetFiles(pathToDirectory, "*.catFile");
		catSelectButtons = new Button[files.Length];
		selectableCats = new CatExportImportData[files.Length];
		for(int i = 0; i < files.Length; i++) {
			string f = files[i];
			string filename = System.IO.Path.GetFileNameWithoutExtension(f);

			// Don't show the starting cat
			if(filename.Equals("startingCat"))
				continue;

			GameObject b = Instantiate(catSelectButtonPrefab) as GameObject;
			
			RectTransform rt = b.GetComponent<RectTransform>();
			rt.SetParent(catSelectButtonParent);
			
			Text t = b.GetComponentInChildren<Text>();
			t.text = f;
			
			CatExportImportData cat = CatExportImport.Instance.ImportCat(filename);
			selectableCats[i] = cat;
			
			b.GetComponent<CatSelectButton>().SetCat(cat);
			Button button = b.GetComponent<Button>();
			button.onClick.AddListener(() => CatSelected(filename, cat));
			
			catSelectButtons[i] = button;
		}
	}
	public void HideDialog() {
		dialogObject.SetActive(false);
	}
}
