using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatManager : MonoBehaviour {
	// A list of all cats currently in play
	private static List<GameObject> cats;

	// Use this for initialization
	void Awake() {
		cats = new List<GameObject>();
	}
	
	// The WorldUpdate message is sent by the WorldUpdate class when the game is not paused
	void WorldUpdate (float deltaTime) {
		foreach(GameObject c in cats) {
			c.SendMessage("CatUpdate", deltaTime);
		}
	}

	public static void AddCat(GameObject c) {
		cats.Add(c);
	}
	public static void RemoveCat(GameObject c) {
		cats.Remove(c);
	}
}
