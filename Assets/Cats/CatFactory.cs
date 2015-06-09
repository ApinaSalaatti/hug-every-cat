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
	private string[] maleNames = new string[] { "Pasi", "Niilo" };
	private string[] femaleNames = new string[] { "Laila", "Viivi" };
	
	public string RandomCatName(Gender gender) {
		switch(gender) {
		case Gender.FEMALE:
			return femaleNames[Random.Range(0, femaleNames.Length)];
		case Gender.MALE:
			return maleNames[Random.Range(0, maleNames.Length)];
		}

		// Unknown gender, just choose at random
		if(Random.value > 0.5f) return RandomCatName(Gender.FEMALE);
		else return RandomCatName(Gender.MALE);
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

	public GameObject CreateFromFile(string filename) {
		return CatExportImport.Instance.ImportCat(filename);
	}
}
