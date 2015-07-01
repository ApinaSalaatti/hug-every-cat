using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private static Player instance;
	public static Player Instance { get { return instance; } }

	private Wallet wallet;
	public Wallet Wallet { get { return wallet; } }

	// Use this for initialization
	void Awake() {
		instance = this;
		wallet = GetComponent<Wallet>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
