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
			c.SendMessage("CatUpdate", deltaTime);
		}
	}

	public void AddCat(GameObject c) {
		cats.Add(c);
	}
	public void RemoveCat(GameObject c) {
		cats.Remove(c);
	}
}
