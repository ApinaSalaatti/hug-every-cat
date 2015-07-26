using UnityEngine;
using System.Collections;

public class CatExportImport : MonoBehaviour {
	private static CatExportImport instance;
	public static CatExportImport Instance {
		get { return instance; }
	}

	void Awake() {
		instance = this;
	}

	/*
	 * Instance stuff starts here
	 */
	private string catSaveFolder = "";

	// Use this for initialization
	void Start () {
		catSaveFolder = Globals.CatFolder;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ExportImage(Sprite catSprite) {

	}

	private string CreateFilename(string catName) {
		double unixTimestamp = (System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
		return catName.Replace(" ", "") + ((long)unixTimestamp).ToString() + ".catFile";
	}

	// Returns the filename where the cat was saved
	public string ExportCat(GameObject cat, string fileName = "") {
		Texture2D tex = cat.GetComponent<SpriteRenderer>().sprite.texture;
		CatStats stats = cat.GetComponent<CatStats>();

		int width = tex.width;
		int height = tex.height;
		string textureString = "";

		for(int y = 0; y < height; y++) {
			string line = "";
			for(int x = 0; x < width; x++) {
				Color c = tex.GetPixel(x,y);
				line += c.r.ToString("0.00") + "," + c.g.ToString("0.00") + "," + c.b.ToString("0.00") + "," + c.a.ToString("0.00");
				if(x < width-1)
					line += ";";
			}
			//textureLines[y] = line;
			textureString += line;
			if(y < height-1)
				textureString += ":";
		}

		if(string.IsNullOrEmpty(fileName))
			fileName = CreateFilename(stats.Name);

		using (System.IO.StreamWriter file = new System.IO.StreamWriter(catSaveFolder + fileName)) {
			string bt = "";
			switch(stats.BodyType) {
			case BodyType.SKINNY:
				bt = "skinny";
				break;
			case BodyType.MEDIUM:
				bt = "medium";
				break;
			case BodyType.FAT:
				bt = "fat";
				break;
			}

			string gender = "";
			switch(stats.Gender) {
			case Gender.MALE:
				gender = "m";
				break;
			case Gender.FEMALE:
				gender = "f";
				break;
			default:
				gender = "u";
				break;
			}

			JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
			json.AddField("name", stats.Name);
			json.AddField("gender", gender);
			json.AddField("bodyType", bt);
			json.AddField("image", textureString);

			file.WriteLine(json.Print());
		}

		return fileName;
	}

	public BodyType GetBodyType(string bt) {
		if(bt.Equals("skinny"))
			return BodyType.SKINNY;
		else if(bt.Equals("fat"))
			return BodyType.FAT;
		else
			return BodyType.MEDIUM;
	}

	public Gender GetGender(string g) {
		if(g.Equals("f"))
			return Gender.FEMALE;
		else if(g.Equals("m"))
			return Gender.MALE;
		else
			return Gender.UNKNOWN;
	}
	
	public GameObject ImportCat(string filename) {
		GameObject cat = Instantiate(Globals.CatPrefab);

		string[] lines = System.IO.File.ReadAllLines(catSaveFolder + filename);

		JSONObject json = new JSONObject(lines[0]);
		string name = json.GetField("name").str;
		string btString = json.GetField("bodyType").str;
		string genderString = json.GetField("gender").str;
		string img = json.GetField("image").str;

		BodyType bt = GetBodyType(btString);
		CatStats stats = cat.GetComponent<CatStats>();
		stats.BodyType = bt;

		stats.Name = name;
		cat.name = name; // Set the name of the gameobject so it's easier to find in the hierarchy...

		stats.Gender = GetGender(genderString);

		Sprite s = CreateSprite(bt, img);
		cat.GetComponent<CatSpriteManager>().SetSprite(s);

		return cat;
	}

	public Sprite CreateSprite(BodyType bt, string img) {
		string[] textureLines = img.Split(new char[] { ':' });

		Sprite emptyCat = null;
		switch(bt) {
		case BodyType.SKINNY:
			emptyCat = Globals.CatImages.Skinny.emptyCat;
			break;
		case BodyType.MEDIUM:
			emptyCat = Globals.CatImages.Medium.emptyCat;
			break;
		case BodyType.FAT:
			emptyCat = Globals.CatImages.Fat.emptyCat;
			break;
		}
		
		Texture2D tex = new Texture2D(emptyCat.texture.width, emptyCat.texture.height, emptyCat.texture.format, false);
		tex.filterMode = FilterMode.Point;
		
		for(int y = 0; y < textureLines.Length; y++) {
			string[] line = textureLines[y].Split(new char[] { ';' });
			for(int x = 0; x < line.Length; x++) {
				string[] color = line[x].Split(new char[] { ',' });
				Color c = new Color(float.Parse(color[0]), float.Parse(color[1]), float.Parse(color[2]), float.Parse(color[3]));
				tex.SetPixel(x, y, c);
			}
		}
		
		tex.Apply();
		
		return Sprite.Create(tex, emptyCat.rect, new Vector2(0.5f, 0.5f), emptyCat.pixelsPerUnit, 1, SpriteMeshType.Tight);
	}
}
