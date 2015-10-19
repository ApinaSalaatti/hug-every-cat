using UnityEngine;
using System.Collections;

public class StartingPoint : MonoBehaviour {

	// Use this for initialization
	void Start() {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.transform.position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
