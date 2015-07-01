using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ColorTool {
	public Slider rSlider;
	public Slider gSlider;
	public Slider bSlider;
}

public class CatCreator : MonoBehaviour {
	/*
     * Used for drawing tools
	 */
	[SerializeField]
	private Text currentToolIndicator;
	// Randomization stuff
	[SerializeField]
	private GameObject randomizationTools;
	[SerializeField]
	private Toggle randomColorToggle;

	[SerializeField]
	private ColorSelector fgSelector;
	[SerializeField]
	private ColorSelector bgSelector;

	private FurRandomizationMethod method = FurRandomizationMethod.CELLULAR_AUTOMATA;
	public FurRandomizationMethod CurrentRandomizationMethod { get { return method; } }
	public void SetRandomizationMethod(int m) {
		switch(m) {
		case 0:
			method = FurRandomizationMethod.CELLULAR_AUTOMATA;
			break;
		case 1:
			method = FurRandomizationMethod.PERLIN_NOISE;
			break;
		}
	}

	private Sprite emptyCat;
	private Sprite legalPixels;

	/*
	 * Used for editing the cat's stats
	 */
	[SerializeField]
	private GameObject statTools;
	[SerializeField]
	private InputField catNameText;
	[SerializeField]
	private Toggle femaleToggle;

	/*
	 * GameObject and and cached components of the cat that's being edited
	 */
	[SerializeField]
	private GameObject cat;
	private Sprite catSprite;
	public Sprite CatSprite { get { return catSprite; } }
	private CatStats stats;

	private Color color = new Color();

	private DrawTool currentTool = DrawTool.PEN;
	private bool usingTool = false;

	// Used tools are queued here for undo/redo
	private List<CatCreatorTool> toolQueue = new List<CatCreatorTool>();
	private int queueIndex = -1;

	/*
	 * The name of the file the cat is currently being saved to
	 */
	private string currentFilename = "";

	void Awake() {
		stats = cat.GetComponent<CatStats>();

		// Set the medium as chosen at the beginning
		SetCatBody(1);

		ResetCat();
	}
	
	// Update is called once per frame
	void Update () {
		// STATS UPDATE
		stats.Name = catNameText.text;
		if(femaleToggle.isOn)
			stats.Gender = Gender.FEMALE;
		else
			stats.Gender = Gender.MALE;

		// DRAWING UPDATE
		color = fgSelector.Color;

		if(usingTool) {
			Debug.Log(queueIndex);
			// Pen will execute constantly until pointer button is released
			if(toolQueue[queueIndex].toolType == DrawTool.PEN) {
				toolQueue[queueIndex].Execute();
			}
		}
	}

	public void RandomizeName() {
		catNameText.text = CatFactory.Instance.RandomCatName(stats.Gender);
	}

	// 0 = skinny, 1 = medium, 2 = fat
	public void SetCatBody(int b) {
		if(b == 0) {
			cat.GetComponent<CatStats>().BodyType = BodyType.SKINNY;
			emptyCat = Globals.CatImages.Skinny.emptyCat;
			legalPixels = Globals.CatImages.Skinny.legalPixels;
		}
		else if(b == 1) {
			cat.GetComponent<CatStats>().BodyType = BodyType.MEDIUM;
			emptyCat = Globals.CatImages.Medium.emptyCat;
			legalPixels = Globals.CatImages.Medium.legalPixels;
		}
		else {
			cat.GetComponent<CatStats>().BodyType = BodyType.FAT;
			emptyCat = Globals.CatImages.Fat.emptyCat;
			legalPixels = Globals.CatImages.Fat.legalPixels;
		}
		ResetCat();
	}
	public void SetCatBody(BodyType bt) {
		cat.GetComponent<CatStats>().BodyType = bt;
		if(bt == BodyType.SKINNY) {
			emptyCat = Globals.CatImages.Skinny.emptyCat;
			legalPixels = Globals.CatImages.Skinny.legalPixels;
		}
		else if(bt == BodyType.MEDIUM) {
			emptyCat = Globals.CatImages.Medium.emptyCat;
			legalPixels = Globals.CatImages.Medium.legalPixels;
		}
		else {
			emptyCat = Globals.CatImages.Fat.emptyCat;
			legalPixels = Globals.CatImages.Fat.legalPixels;
		}
		ResetCat();
	}

	// Returns true if the given pixel can be modified
	public bool IsLegal(int x, int y) {
		if(x < 0 || x >= legalPixels.texture.width || y < 0 || y >= legalPixels.texture.height)
			return false;

		return legalPixels.texture.GetPixel(x, y).a > 0f;
	}

	public void ResetCat() {
		CopySpriteData(emptyCat);

		toolQueue.Clear();
		queueIndex = -1;
		usingTool = false;
	}
	public void CopySpriteData(Sprite s) {
		Texture2D tex = new Texture2D(s.texture.width, s.texture.height, s.texture.format, false);
		tex.filterMode = FilterMode.Point;
		tex.SetPixels(s.texture.GetPixels());
		tex.Apply();
		catSprite = Sprite.Create(tex, s.rect, new Vector2(0.5f, 0.5f), s.pixelsPerUnit, 1, SpriteMeshType.Tight);
		cat.GetComponent<SpriteRenderer>().sprite = catSprite;
	}

	public void ShowRandomizationTools() {
		randomizationTools.SetActive(true);
	}
	public void HideRandomizationTools() {
		randomizationTools.SetActive(false);
	}

	public void SetTool(int t) {
		switch(t) {
		case 0: currentTool = DrawTool.PEN; currentToolIndicator.text = "PEN"; break;
		case 1: currentTool = DrawTool.BUCKET; currentToolIndicator.text = "BUCKET"; break;
		case 2: currentTool = DrawTool.COLOR_PICKER; currentToolIndicator.text = "PICKER"; break;
		case 3: currentTool = DrawTool.RANDOM_FUR; currentToolIndicator.text = "RANDOMIZE"; break;
		}
	}

	public void UndoTool() {
		if(queueIndex > -1) {
			toolQueue[queueIndex].UndoExecute();
			queueIndex--;
		}
	}
	public void RedoTool() {
		if(queueIndex < toolQueue.Count-1) {
			queueIndex++;
			toolQueue[queueIndex].RedoExecute();
		}
	}
	
	public void SetColor(float r, float g, float b) {
		fgSelector.SetColor(r, g, b);
	}

	public void StartTool() {
		// The color picker is a special tool that doesn't go to the queue
		if(currentTool == DrawTool.COLOR_PICKER) {
			ColorPickerTool picker = new ColorPickerTool(this);
			picker.Execute();
			return;
		}

		// Discard any undoed actions
		int toDiscard = (toolQueue.Count - 1) - queueIndex;
		if(toDiscard > 0) {
			toolQueue.RemoveRange(queueIndex+1, toDiscard);
		}

		usingTool = false;
		switch(currentTool) {
		case DrawTool.PEN:
			usingTool = true; // Pen is used during multiple frames
			toolQueue.Add(new PenTool(this, color));
			break;
		case DrawTool.BUCKET:
			toolQueue.Add(new BucketTool(this, color));
			break;
		case DrawTool.RANDOM_FUR:
			toolQueue.Add(new RandomFurTool(this, CurrentRandomizationMethod, randomColorToggle.isOn,
				fgSelector.Color,
			    bgSelector.Color
			));
			// Because the randomizer is not really a drawing tool, we reset the tool after every usage
			SetTool(0);
			break;
		}

		// Set the index at the last item
		queueIndex = toolQueue.Count-1;

		// Execute the tool
		toolQueue[queueIndex].Execute();
	}
	public void EndTool() {
		usingTool = false;
	}

	public Color GetColorAt(int pixelX, int pixelY) {
		Color c = catSprite.texture.GetPixel(pixelX, pixelY);
		return new Color(c.r, c.g, c.b, c.a); // Copy the color so the references won't get all crazy and mixed up and whooa
	}
	public void SetColorAt(int pixelX, int pixelY, Color c) {
		catSprite.texture.SetPixel(pixelX, pixelY, c);
		catSprite.texture.Apply();
	}

	// A helper method to check if two colors are the same
	public bool SameColor(Color col1, Color col2) {
		//Debug.Log(col1.ToString() + " VS " + col2.ToString());
		if(col1.Equals(col2) || col1 == col2) {
			return true;
		}
		else if(col1.r == col2.r && col1.g == col2.g && col1.b == col2.b && col1.a == col2.a) {
			return true;
		}
		return false;
	}
	
	public int GetMousePixelX() {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = cat.transform.position.z;
		mousePos = cat.transform.InverseTransformPoint(mousePos);
		
		// A correction because of the pivot point being at the center
		mousePos.x += 0.5f;

		return Mathf.FloorToInt(mousePos.x * catSprite.texture.width);
	}
	public int GetMousePixelY() {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = cat.transform.position.z;
		mousePos = cat.transform.InverseTransformPoint(mousePos);
		
		// A correction because of the pivot point being at the center
		mousePos.y += 0.5f; 

		return Mathf.FloorToInt(mousePos.y * catSprite.texture.height);
	}

	public void ShowStatTools() {
		statTools.transform.localScale = new Vector3(1f, 1f, 1f);
	}
	public void HideStatTools() {
		statTools.transform.localScale = Vector3.zero;
	}
	
	public void SaveCatData() {
		currentFilename = CatExportImport.Instance.ExportCat(cat, currentFilename);
	}

	private void OnCatSave() {

	}

	public void LoadCatData() {
		CatLoadDialog.Instance.catSelectedListeners += OnCatLoad;
		CatLoadDialog.Instance.cancelListeners += OnLoadCancel;
		CatLoadDialog.Instance.ShowDialog(Globals.CatFolder);
	}

	private void OnLoadCancel() {
		CatLoadDialog.Instance.catSelectedListeners -= OnCatLoad;
		CatLoadDialog.Instance.cancelListeners -= OnLoadCancel;
	}

	private void OnCatLoad(string filename, GameObject c) {
		CatLoadDialog.Instance.catSelectedListeners -= OnCatLoad; // No need to listen anymore
		CatLoadDialog.Instance.cancelListeners -= OnLoadCancel;

		currentFilename = filename;
		catNameText.text = c.GetComponent<CatStats>().Name;
		
		BodyType bt = c.GetComponent<CatStats>().BodyType;
		SetCatBody(bt);

		ResetCat();

		CopySpriteData(c.GetComponent<CatSpriteManager>().GetSprite());
	}

	public void FinalizeCat() {
		// Serialize the cat so it can be loaded at game start
		CatExportImport.Instance.ExportCat(cat, "startingCat");
		Application.LoadLevel(2);

		//GameObject c = Instantiate(Globals.CatPrefab) as GameObject;
		//c.GetComponent<CatSpriteManager>().SetSprite(cat.GetComponent<SpriteRenderer>().sprite);
	}
}
