using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HouseItemManager : MonoBehaviour {
	private static HouseItemManager instance;
	public static HouseItemManager Instance { get { return instance; } }
	
	private List<GameObject> items;
	
	// Use this for initialization
	void Awake() {
		instance = this;
		
		items = new List<GameObject>();
	}
	
	// The WorldUpdate message is sent by the WorldUpdate class when the game is not paused
	void WorldUpdate (float deltaTime) {
		foreach(GameObject i in items) {
			i.SendMessage("WorldUpdate", deltaTime);
		}
	}
	
	public List<GameObject> GetItems() {
		return items;
	}
	public void AddItem(GameObject i) {
		items.Add(i);
		i.BroadcastMessage("OnAddedToWorld", SendMessageOptions.DontRequireReceiver);
		GameEventManager.Instance.QueueEvent(GameEvent.HOUSE_ITEM_ADDED, i);
	}
	public void RemoveItem(GameObject i) {
		items.Remove(i);
		i.BroadcastMessage("OnRemovedFromWorld", SendMessageOptions.DontRequireReceiver);
		GameEventManager.Instance.QueueEvent(GameEvent.HOUSE_ITEM_REMOVED, i);
	}
	
	public void SaveGame() {
		JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
		JSONObject itemList = new JSONObject(JSONObject.Type.ARRAY);
		
		foreach(GameObject item in items) {
			JSONObject i = new JSONObject(JSONObject.Type.OBJECT);
			JSONObject tr = new JSONObject(JSONObject.Type.OBJECT);
			tr.AddField("position", item.transform.position.ToString());
			i.AddField("transform", tr);

			string resource = item.GetComponent<LoadInfo>().Resource;
			i.AddField("resource", resource);

			//JSONObject specialComponents = new JSONObject(JSONObject.Type.ARRAY);
			//i.AddField("specialComponents", specialComponents);
			item.BroadcastMessage("Save", i);
			itemList.Add(i);
		}
		
		json.AddField("items", itemList);
		using(System.IO.StreamWriter file = new System.IO.StreamWriter(Globals.SaveFolder + "houseItems")) {
			file.WriteLine(json.Print());
		}
	}
	public void LoadGame() {
		Debug.Log("HouseItemManager loading");
		string[] lines = System.IO.File.ReadAllLines(Globals.SaveFolder + "houseItems");
		JSONObject json = new JSONObject(lines[0]);
		JSONObject itemList = json.GetField("items");
		foreach(JSONObject i in itemList.list) {
			GameObject item = HouseItemFactory.Instance.LoadFromJSON(i);
			items.Add(item);
		}
	}
}
