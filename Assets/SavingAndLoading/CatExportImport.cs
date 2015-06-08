using UnityEngine;
using System.Collections;

public class CatExportImport : MonoBehaviour {
	[SerializeField]
	private GameObject catPrefab;

	private string catSaveFolder = "";

	// Use this for initialization
	void Start () {
		catSaveFolder = Globals.CatFolder;
		System.IO.Directory.CreateDirectory(catSaveFolder);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ExportImage(Sprite catSprite) {

	}

	private string CreateFilename(string catName) {
		double unixTimestamp = (System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
		return catName + ((long)unixTimestamp).ToString() + ".cat";
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

			JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
			json.AddField("name", stats.Name);
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
	
	public GameObject ImportCat(string pathToFile) {
		GameObject cat = Instantiate(catPrefab);

		string[] lines = System.IO.File.ReadAllLines(pathToFile);

		JSONObject json = new JSONObject(lines[0]);
		string name = json.GetField("name").Print().Replace("\"", "");
		string btString = json.GetField("bodyType").Print().Replace("\"", "");
		string img = json.GetField("image").Print().Replace("\"", "");

		BodyType bt = GetBodyType(btString);
		CatStats stats = cat.GetComponent<CatStats>();
		stats.BodyType = bt;
		stats.Name = name;

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
