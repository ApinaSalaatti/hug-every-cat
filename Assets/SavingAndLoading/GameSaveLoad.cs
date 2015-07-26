using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

	private List<ISaveable> saveables;
	
	// Use this for initialization
	void Awake() {
		instance = this;
		savePresent = File.Exists(Globals.SaveFolder + "saveInfo");
		Debug.Log("Looking for save from " + Globals.SaveFolder);
		Debug.Log("Save found: " + savePresent);

		saveables = new List<ISaveable>();
	}

	public void AddSaveable(ISaveable s) {
		saveables.Add(s);
	}
	public void RemoveSaveabe(ISaveable s) {
		saveables.Remove(s);
	}

	// Informs all saveables to "start from scratch"
	public void StartNewGame() {
		foreach(ISaveable s in saveables) {
			Debug.Log("Calling StartNewGame of " + s);
			s.StartNewGame();
		}
	}
	
	public void SaveGame() {
		using(StreamWriter file = new StreamWriter(Globals.SaveFolder + "saveInfo")) {
			JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
			json.AddField("time", System.DateTime.Now.ToString("dd MMM yyyy HH:mm"));
			file.WriteLine(json.Print());
		}

		foreach(ISaveable s in saveables) {
			s.SaveGame();
		}
		//CatManager.Instance.SaveGame();
		//HouseItemManager.Instance.SaveGame();
	}
	
	public void LoadGame() {
		if(savePresent) {
			foreach(ISaveable s in saveables) {
				s.LoadGame();
			}
			//CatManager.Instance.LoadGame();
			//HouseItemManager.Instance.LoadGame();
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

