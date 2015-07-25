using UnityEngine;
using System.Collections;
using System.IO;

public enum WorldInitMode { NEW, LOAD }

public class WorldInit : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		//HouseItem food = FindObjectOfType<HouseItem>();
		//HouseItemManager.Instance.AddItem(food.gameObject);
		
		if(File.Exists(Globals.TempDataFolder + "init")) {
			string[] lines = File.ReadAllLines(Globals.TempDataFolder + "init");
			JSONObject json = new JSONObject(lines[0]);
			string type = json.GetField("type").str;
			if(type.Equals("load")) {
				InitLoad();
			}
			else {
				InitNew();
			}
		}
		else {
			InitNew();
		}
	}
	
	private void InitNew() {
		Debug.Log("Initing new game");

		GameSaveLoad.Instance.StartNewGame();
	}
	private void InitLoad() {
		Debug.Log("Initing load game");
		GameSaveLoad.Instance.LoadGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public static void CreateInitFile(WorldInitMode mode) {
		switch(mode) {
		case WorldInitMode.NEW:
			CreateNewGameInitFile();
			break;
		case WorldInitMode.LOAD:
			CreateLoadGameInitFile();
			break;
		}
	}
	
	private static void CreateNewGameInitFile() {
		using(StreamWriter file = new StreamWriter(Globals.TempDataFolder + "init")) {
			JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
			json.AddField("type", "new");
			file.WriteLine(json.Print());
		}
	}
	private static void CreateLoadGameInitFile() {
		using(StreamWriter file = new StreamWriter(Globals.TempDataFolder + "init")) {
			JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
			json.AddField("type", "load");
			file.WriteLine(json.Print());
		}
	}
}
