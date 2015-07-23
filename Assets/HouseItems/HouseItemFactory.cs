using UnityEngine;
using System.Collections;

public class HouseItemFactory : MonoBehaviour {
	private static HouseItemFactory instance;
	public static HouseItemFactory Instance { get { return instance; } }
	
	[SerializeField]
	private GameObject itemPrefab;
	
	void Awake() {
		instance = this;
	}
	
	public GameObject CreateEmpty() {
		return Instantiate(itemPrefab);
	}
	
	public GameObject CreateItemFromResource(string resourceName) {
		return Instantiate(Resources.Load<GameObject>(resourceName));
	}
	
	public GameObject LoadFromJSON(JSONObject json) {
		//GameObject item = CreateEmpty();
		
		//JSONObject specialComponents = json.GetField("specialComponents");
		//foreach(JSONObject component in specialComponents.list) {
		//	//Debug.Log(component.str);
		//	AddSpecialComponent(item, component.str);
		//}

		string resource = json.GetField("resource").str;
		GameObject item = CreateItemFromResource(resource);

		// TODO: Make this somehow less hacky
		JSONObject tr = json.GetField("transform");
		string posString = tr.GetField("position").str;
		posString = posString.Replace("(", "");
		posString = posString.Replace(")", "");
		Debug.Log(posString);
		string[] coords = posString.Split(',');
		Vector3 pos = new Vector3(float.Parse(coords[0]), float.Parse(coords[1]), float.Parse(coords[2]));
		item.transform.position = pos;
		
		item.BroadcastMessage("Load", json);
		
		return item;
	}
	private void AddSpecialComponent(GameObject obj, string comp) {
		switch(comp) {
		case "camera":
			obj.AddComponent<CameraItem>();
			break;
		}
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
