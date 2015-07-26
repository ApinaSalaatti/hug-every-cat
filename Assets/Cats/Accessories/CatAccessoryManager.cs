using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AccessoryData {
	public string name;
	public Sprite sprite;
}

public class CatAccessoryManager : MonoBehaviour {
	private static CatAccessoryManager instance;
	public static CatAccessoryManager Instance { get { return instance; } }

	[SerializeField]
	private AccessoryData[] allHats;

	private List<string> availableHats;
	public List<string> AvailableHats { get { return availableHats; } }

	// Use this for initialization
	void Awake() {
		instance = this;

		availableHats = new List<string>();
		availableHats.Add("Top Hat");
	}

	public bool HatAvailable(string hat) {
		return availableHats.Contains(hat);
	}

	public Sprite GetHat(string hat) {
		foreach(AccessoryData data in allHats) {
			if(data.name == hat) {
				return data.sprite;
			}
		}
		return null;
	}

	public void AddAvailableHat(string hat) {
		availableHats.Add(hat);
	}
}
