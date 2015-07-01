using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatPerceptions : MonoBehaviour, IGameEventListener {
	private NeedMonitor needMonitor;
	public NeedMonitor NeedMonitor { get { return needMonitor; } }

	private List<GameObject> hungerItems;
	private List<GameObject> cleanlinessItems;
	private List<GameObject> happinessItems;
	private List<GameObject> energyItems;

	private List<GameObject> uncheckedItems;

	void Start() {
		hungerItems = new List<GameObject>();
		cleanlinessItems = new List<GameObject>();
		happinessItems = new List<GameObject>();
		energyItems = new List<GameObject>();

		uncheckedItems = new List<GameObject>();

		needMonitor = GetComponent<NeedMonitor>();

		GameEventManager.Instance.RegisterListener(GameEvent.HOUSE_ITEM_ADDED, this);
		GameEventManager.Instance.RegisterListener(GameEvent.HOUSE_ITEM_REMOVED, this);

		// When the cat comes to the house, she hasn't inspected any items
		uncheckedItems.AddRange(HouseItemManager.Instance.GetItems());
	}

	void CatUpdate(float deltaTime) {

	}

	public bool ItemIsUnchecked(GameObject o) {
		return uncheckedItems.Contains(o);
	}
	public bool UncheckedItemsLeft() {
		return uncheckedItems.Count > 0;
	}

	private GameObject GetUsableItem(List<GameObject> list, bool closest = true) {
		float closestDist = float.MaxValue;
		GameObject c = null;
		foreach(GameObject item in list) {
			if(item.GetComponent<HouseItem>().HasSpace()) {
				if(closest) {
					float d = Vector3.Distance(gameObject.transform.position, item.transform.position);
					if(d < closestDist) {
						closestDist = d;
						c = item;
					}
				}
				else {
					// If we don't care about the distance, just return the first one that has space
					return item;
				}
			}
		}
		return c;
	}

	public GameObject GetUncheckedItem(bool closest = true) {
		// Here we don't care about the space because this is used when the cat just wants to examine something
		if(!closest) {
			return uncheckedItems[0];
		}

		float closestDist = float.MaxValue;
		GameObject c = null;
		foreach(GameObject item in uncheckedItems) {
			float d = Vector3.Distance(gameObject.transform.position, item.transform.position);
			if(d < closestDist) {
				closestDist = d;
				c = item;
			}
		}
		return c;
	}

	public GameObject GetHungerItem(bool closest = true) {
		return GetUsableItem(hungerItems, closest);
	}
	public GameObject GetCleanlinessItem(bool closest = true) {
		return GetUsableItem(cleanlinessItems, closest);
	}
	public GameObject GetHappinessItem(bool closest = true) {
		return GetUsableItem(happinessItems, closest);
	}
	public GameObject GetEnergyItem(bool closest = true) {
		return GetUsableItem(energyItems, closest);
	}

	public GameObject GetItemThatSatisfiesNeed(CatNeedType need, bool closest = true) {
		switch(need) {
		case CatNeedType.HUNGER:
			return GetHungerItem(closest);
		case CatNeedType.CLEANLINESS:
			return GetCleanlinessItem(closest);
		case CatNeedType.HAPPINESS:
			return GetHappinessItem(closest);
		case CatNeedType.ENERGY:
			return GetEnergyItem(closest);
		}
		return null;
	}
	public bool CanSatisfyNeed(CatNeedType need) {
		return GetItemThatSatisfiesNeed(need, false) != null;
	}

	public void CheckItem(GameObject item) {
		CheckItem(item.GetComponent<HouseItem>());
	}
	public void CheckItem(HouseItem hi) {
		foreach(CatNeedType t in hi.GetSatisfiedNeeds()) {
			if(t.Equals(CatNeedType.HUNGER))
				hungerItems.Add(hi.gameObject);
			if(t.Equals(CatNeedType.CLEANLINESS))
				cleanlinessItems.Add(hi.gameObject);
			if(t.Equals(CatNeedType.HAPPINESS))
				happinessItems.Add(hi.gameObject);
			if(t.Equals(CatNeedType.ENERGY))
				energyItems.Add(hi.gameObject);
		}

		uncheckedItems.Remove(hi.gameObject);
	}

	public void ReceiveEvent(GameEvent e) {
		if(e.GameEventType == GameEvent.HOUSE_ITEM_ADDED) {
			GameObject item = e.GameEventData as GameObject;
			uncheckedItems.Add(item);
			gameObject.SendMessage("AIMessage", new AIMessage(AIMessageType.NEW_INTERESTING_OBJECT, item));
		}
		if(e.GameEventType == GameEvent.HOUSE_ITEM_REMOVED) {
			// Remove from all lists
			GameObject item = e.GameEventData as GameObject;
			hungerItems.Remove(item);
			cleanlinessItems.Remove(item);
			happinessItems.Remove(item);
			energyItems.Remove(item);
			uncheckedItems.Remove(item);
		}
	}

	/* 
	 * ================================
	 * =    FOR DEBUGGING PURPOSES    =
	 * ================================
	 */
	public List<GameObject> Debug_GetUncheckedItems() {
		return uncheckedItems;
	}

	public List<GameObject> Debug_GetNeedItems(CatNeedType need) {
		switch(need) {
		case CatNeedType.HUNGER:
			return hungerItems;
		}

		return null;
	}
}
