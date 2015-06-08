using UnityEngine;
using System.Collections;

public class WorldUpdate : MonoBehaviour {
	private bool paused = false;
	public bool Paused { get { return paused; } }

	public void PauseGame() {
		paused = true;
	}
	public void UnpauseGame() {
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
