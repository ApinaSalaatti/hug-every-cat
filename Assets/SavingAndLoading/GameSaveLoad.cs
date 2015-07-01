using UnityEngine;
using System.Collections;
using System.IO;

public class SaveInfo {
	public string time;

	public SaveInfo(string t) {
		time = t;
	}
}

public class GameSaveLoad : MonoBehaviour {
	private static GameSaveLoad instance;
	public static GameSaveLoad Instance { get { return instance; } }

	private bool savePresent = false;
	public bool SavePresent { get { return savePresent; } }

	// Use this for initialization
	void Awake() {
		instance = this;
		savePresent = File.Exists(Globals.SaveFolder + "saveInfo");
	}

	public void SaveGame() {
		using(StreamWriter file = new StreamWriter(Globals.SaveFolder + "saveInfo")) {
			JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
			json.AddField("time", System.DateTime.Now.ToString("dd MMM yyyy HH:mm"));
			file.WriteLine(json.Print());
		}

		CatManager.Instance.SaveGame();
	}

	public void LoadGame() {
		if(savePresent) {
			CatManager.Instance.LoadGame();
		}
	}

	public SaveInfo GetSaveInfo() {
		if(savePresent) {
			string[] lines = System.IO.File.ReadAllLines(Globals.SaveFolder + "saveInfo");
			JSONObject json = new JSONObject(lines[0]);
			return new SaveInfo(json.GetField("time").str);
		}

		return null;
	}
}
