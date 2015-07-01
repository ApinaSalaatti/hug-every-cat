using UnityEngine;
using System.Collections;

public class HouseItemFactory : MonoBehaviour {
	private static HouseItemFactory instance;
	public static HouseItemFactory Instance { get { return instance; } }

	void Awake() {
		instance = this;
	}

	public GameObject CreateItemFromResource(string resourceName) {
		return Instantiate(Resources.Load<GameObject>(resourceName));
	}

	/*
	public GameObject CreateItemFromFile(string file) {
		string[] lines = System.IO.File.ReadAllLines(catSaveFolder + filename);
		JSONObject json = new JSONObject(lines[0]);
		return CreateItemFromJSON(json);
	}
	public GameObject CreateItemFromJSON(JSONObject json) {
		GameObject item = Instantiate(prefab);

		JSONObject script = json.GetField("script");
		HouseItem hi = item.GetComponent<HouseItem>();
		hi.CreateFromJSON(script);

		return item;
	}*/
}
