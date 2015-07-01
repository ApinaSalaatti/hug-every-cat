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
		i.SendMessage("OnAddedToWorld", SendMessageOptions.DontRequireReceiver);
		GameEventManager.Instance.QueueEvent(GameEvent.HOUSE_ITEM_ADDED, i);
	}
	public void RemoveItem(GameObject i) {
		items.Remove(i);
		i.SendMessage("OnRemovedFromWorld", SendMessageOptions.DontRequireReceiver);
		GameEventManager.Instance.QueueEvent(GameEvent.HOUSE_ITEM_REMOVED, i);
	}
}
