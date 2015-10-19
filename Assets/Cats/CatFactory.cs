using UnityEngine;
using System.Collections;

public class CatFactory : MonoBehaviour {
	private static CatFactory instance;
	public static CatFactory Instance {
		get { return instance; }
	}
	
	void Awake() {
		instance = this;
	}
	
	/*
	 * Actual instance stuff starts here
	 */
	private string[] maleNames = new string[] { "Mr.", "Sir", "John" };
	private string[] femaleNames = new string[] { "Mrs.", "Ms.", "Madam", "Lissie", "Gwyneth" };
	private string[] genderlessNames = new string[] { "Camembert", "Taco" };
	private string[] lastNames = new string[] { "Cattington", "Catman", "von Catberg", "Whiskers" };
	
	public string RandomCatName(Gender gender) {
		string name = "";
		switch(gender) {
		case Gender.FEMALE:
			if(Random.value > 0.8f)
				name = genderlessNames[Random.Range(0, genderlessNames.Length)];
			else
				name = femaleNames[Random.Range(0, femaleNames.Length)];
			break;
		case Gender.MALE:
			if(Random.value > 0.8f)
				name = genderlessNames[Random.Range(0, genderlessNames.Length)];
			else
				name = maleNames[Random.Range(0, maleNames.Length)];
			break;
		case Gender.UNKNOWN:
			// Unknown gender, just choose at random
			if(Random.value > 0.5f) return RandomCatName(Gender.FEMALE);
			else return RandomCatName(Gender.MALE);
		}

		if(Random.value > 0.7f) {
			// Add a last name!
			name += " " + lastNames[Random.Range(0, lastNames.Length)];
		}

		return name;
	}
	
	public Gender RandomGender() {
		if(Random.value > 0.5f) return Gender.FEMALE;
		else return Gender.MALE;
	}
	
	public BodyType RandomBodyType() {
		float r = Random.value;
		if(r < 0.3f)
			return BodyType.SKINNY;
		else if(r < 0.7f)
			return BodyType.MEDIUM; // Chance of the medium type is a bit larger than the others'
		else
			return BodyType.FAT;
	}
	
	public GameObject CreateEmpty() {
		return Instantiate(Globals.CatPrefab);
	}
	
	public GameObject CreateRandom() {
		GameObject cat = Instantiate(Globals.CatPrefab);
		
		Gender g = RandomGender();
		string name = RandomCatName(g);
		BodyType bt = RandomBodyType();
		CatStats stats = cat.GetComponent<CatStats>();
		cat.name = name;
		stats.Name = name;
		stats.Gender = g;
		stats.BodyType = bt;
		
		cat.GetComponent<CatSpriteManager>().SetSprite(CreateRandomSprite(bt));
		
		return cat;
	}
	
	public GameObject LoadFromJSON(JSONObject json) {
		GameObject cat = CreateEmpty();
		
		cat.BroadcastMessage("Load", json);
		
		return cat;
	}
	
	private Sprite CreateRandomSprite(BodyType bt) {
		RandomFur.UseRandomColors = true;
		Sprite catSprite = Globals.CatImages.GetNewSprite(bt);
		Color[,] cols = RandomFur.CellularAutomata(catSprite.texture.width, catSprite.texture.height);
		
		for(int y = 0; y < cols.GetLength(0); y++) {
			for(int x = 0; x < cols.GetLength(1); x++) {
				if(Globals.CatImages.IsLegal(x, y, bt)) {
					catSprite.texture.SetPixel(x, y, cols[y, x]);
				}
			}
		}
		catSprite.texture.Apply();
		
		return catSprite;
	}

	[SerializeField]
	private Texture2D furTexture;
	[SerializeField]
	private Texture2D legalPixelsTexture;

	public Texture CreateRandomTexture(FurRandomizationMethod method = FurRandomizationMethod.RANDOM_COLOR) {
		Texture2D tex = new Texture2D(furTexture.width, furTexture.height, furTexture.format, true);
		tex.filterMode = FilterMode.Point;
		tex.SetPixels(furTexture.GetPixels());
		tex.Apply();

		if(method == FurRandomizationMethod.CELLULAR_AUTOMATA) {
			RandomFur.UseRandomColors = true;
			Color[,] cols = RandomFur.CellularAutomata(tex.width, tex.height);
			for(int y = 0; y < cols.GetLength(0); y++) {
				for(int x = 0; x < cols.GetLength(1); x++) {
					if(IsLegal(x, y)) {
						tex.SetPixel(x, y, cols[y, x]);
					}
				}
			}
		}
		else {
			Color col = new Color(Random.value, Random.value, Random.value);
			for(int y = 0; y < tex.height; y++) {
				for(int x = 0; x < tex.width; x++) {
					if(IsLegal(x, y)) {
						tex.SetPixel(x, y, col);
					}
				}
			}
		}

		tex.Apply();

		return tex;
	}

	public bool IsLegal(int x, int y) {
		if(x < 0 || x >= legalPixelsTexture.width || y < 0 || y >= legalPixelsTexture.height)
			return false;
		
		return legalPixelsTexture.GetPixel(x, y).a > 0f;
	}
	
	public GameObject CreateFromJSON(JSONObject json) {
		return CreateEmpty();
	}
	
	public CatExportImportData CreateFromFile(string filename) {
		return CatExportImport.Instance.ImportCat(filename);
	}
}
