﻿using UnityEngine;
using System.Collections;

public class WorldUpdate : MonoBehaviour {
	private static bool paused = false;
	public static bool Paused { get { return paused; } }

	public static void PauseGame() {
		paused = true;
	}
	public static void UnpauseGame() {
		paused = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!paused) {
			float deltaTime = Time.deltaTime;
			gameObject.SendMessage("WorldUpdate", deltaTime);
		}
	}
}
