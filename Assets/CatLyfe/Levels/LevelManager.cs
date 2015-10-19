using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	private static LevelManager instance;
	public static LevelManager Instance { get { return instance; } }

	private Dictionary<string, int> levelNameToNumberMap;

	private List<string> unlockedLevels;
	public List<string> Unlocked { get { return unlockedLevels; } }

	//private Dictionary<string, int> scores;

	private string dataPath;
	private JSONObject dataAsJSON;

	void Awake() {
		instance = this;

		levelNameToNumberMap = new Dictionary<string, int>();
		PopulateLevelNameToNumberMap();

		unlockedLevels = new List<string>();

		//scores = new Dictionary<string, int>();

		dataPath = Globals.SaveFolder + "levels";
		if(GameSaveLoad.Instance.FileExists(dataPath)) {
			Debug.Log("Loading level data...");
			// A save found, let's see what it contains!
			string data = GameSaveLoad.Instance.ReadFromFile(dataPath, false);
			dataAsJSON = new JSONObject(data);

			// Unlock the already unlocked levels
			JSONObject unl = dataAsJSON.GetField("unlocked");
			for(int i = 0; i < unl.list.Count; i++) {
				unlockedLevels.Add(unl.list[i].str);
			}

			// Set scores
			//JSONObject scoresJSON = dataAsJSON.GetField("scores");
			//for(int i = 0; i < scoresJSON.keys.Count; i++) {
			//	JSONObject score = scoresJSON.GetField(scoresJSON.keys[i]);
			//	int s = (int)score.f;
			//	scores.Add(scoresJSON.keys[i], s);
			//}
		}
		else {
			Debug.Log("Creating new level data...");
			// No levels data saved! Must be a first game or something, so let's write the default data to the file
			dataAsJSON = new JSONObject(JSONObject.Type.OBJECT);
			JSONObject unl = new JSONObject(JSONObject.Type.ARRAY);
		 	dataAsJSON.AddField("unlocked", unl); // Empty array of unlocked levels

			// Add empty score dictionary
			dataAsJSON.AddField("scores", new JSONObject(JSONObject.Type.OBJECT));
			// Add empty saved cats dictionary
			dataAsJSON.AddField("cats", new JSONObject(JSONObject.Type.OBJECT));

			// Unlock the first level
			UnlockLevel("Back Yard");

			// Write to file
			GameSaveLoad.Instance.WriteToFile(dataPath, dataAsJSON.Print(), false);
			GameSaveLoad.Instance.SaveGame();
		}
	}

	// This creates a map of level name to scene number. They are just hard coded values, the names being what the player is shown on level select screen and the numbers the number of the Unity Scene
	private void PopulateLevelNameToNumberMap() {
		levelNameToNumberMap.Add("Back Yard", 8);
		levelNameToNumberMap.Add("Apartments 1", 9);
		levelNameToNumberMap.Add("Apartments 2", 10);
		levelNameToNumberMap.Add("Elevator Shaft", 11);
		levelNameToNumberMap.Add("Offices 1", 12);
		levelNameToNumberMap.Add("Offices 2", 13);
		levelNameToNumberMap.Add("R & D", 14);
	}

	private void UnlockLevel(string level) {
		// Add to currently unlocked levels if not already
		if(!unlockedLevels.Contains(level)) {
			unlockedLevels.Add(level);
			dataAsJSON.GetField("unlocked").Add(level);
		}
	}

	public void LevelCleared(string level, int score, string nextLevel = "") {
		if(nextLevel != null && nextLevel != "") {
			UnlockLevel(nextLevel);
		}

		SetLevelScore(level, score);

		// Save new data instantly
		GameSaveLoad.Instance.WriteToFile(dataPath, dataAsJSON.Print(), false);
		GameSaveLoad.Instance.SaveGame();
	}

	public void SetCatSaved(string level, bool saved) {
		JSONObject cats = dataAsJSON.GetField("cats");
		cats.SetField(level, saved);

		// Save new data instantly
		GameSaveLoad.Instance.WriteToFile(dataPath, dataAsJSON.Print(), false);
		GameSaveLoad.Instance.SaveGame();
	}
	public bool GetCatSaved(string level) {
		JSONObject cats = dataAsJSON.GetField("cats");
		if(cats.HasField(level)) {
			return cats.GetField(level).b;
		}
		else {
			return false;
		}
	}

	public void SetLevelScore(string level, int score) {
		JSONObject scores = dataAsJSON.GetField("scores");
		scores.SetField(level, score);
	}
	public int GetLevelScore(string level) {
		JSONObject scores = dataAsJSON.GetField("scores");
		if(scores.HasField(level)) {
			return (int)scores.GetField(level).n;
		}
		else {
			return 0;
		}
	}

	public void OpenLevel(string level) {
		int l;
		if(levelNameToNumberMap.TryGetValue(level, out l)) {
			MainSceneLoader.currentLevelName = level;
			MainSceneLoader.sceneToLoad = l;
			Application.LoadLevel(5);
		}
	}
}
