using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatManager : MonoBehaviour {
	private static CatManager instance;
	public static CatManager Instance { get { return instance; } }

	// A list of all cats currently in play
	private List<GameObject> cats;

	// Use this for initialization
	void Awake() {
		instance = this;

		cats = new List<GameObject>();
	}
	
	// The WorldUpdate message is sent by the WorldUpdate class when the game is not paused
	void WorldUpdate (float deltaTime) {
		foreach(GameObject c in cats) {
			c.BroadcastMessage("CatUpdate", deltaTime);
		}
	}

	public void AddCat(GameObject c) {
		cats.Add(c);
	}
	public void RemoveCat(GameObject c) {
		cats.Remove(c);
	}

	public void SaveGame() {
		JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
		JSONObject catList = new JSONObject(JSONObject.Type.ARRAY);
		foreach(GameObject cat in cats) {
			JSONObject c = new JSONObject(JSONObject.Type.OBJECT);

			JSONObject stats = new JSONObject(JSONObject.Type.OBJECT);
			cat.GetComponent<CatStats>().Save(stats);
			c.AddField("stats", stats);

			catList.Add(c);
		}
		json.AddField("cats", catList);

		using(System.IO.StreamWriter file = new System.IO.StreamWriter(Globals.SaveFolder + "cats")) {
			file.WriteLine(json.Print());
		}
	}
	public void LoadGame() {
		if(GameSaveLoad.Instance.SavePresent) {
			string[] lines = System.IO.File.ReadAllLines(Globals.SaveFolder + "cats");
			JSONObject json = new JSONObject(lines[0]);
			json.GetField("cats");
		}
	}
}
