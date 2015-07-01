using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	private static World instance;
	public static World Instance { get { return instance; } }

	private WorldTime time;
	public WorldTime Time { get { return time; } }

	// Use this for initialization
	void Awake () {
		instance = this;
		time = GetComponent<WorldTime>();
	}

	void Start() {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
