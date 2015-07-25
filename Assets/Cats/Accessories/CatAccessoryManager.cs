using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatAccessoryManager : MonoBehaviour, ISaveable {
	private CatAccessoryManager instance;
	public CatAccessoryManager Instance { get { return instance; } }

	[SerializeField]
	private Dictionary<string, Sprite> allHats;

	private List<string> availableHats;

	// Use this for initialization
	void Awake() {
		instance = this;

		//allHats = new Dictionary<string, Sprite>();
	}

	public Sprite GetHat(string hat) {
		return allHats[hat];
	}

	public void StartNewGame() {

	}

	public void SaveGame() {

	}

	public void LoadGame() {

	}
}
