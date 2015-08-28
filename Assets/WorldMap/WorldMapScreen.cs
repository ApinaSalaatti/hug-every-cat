using UnityEngine;
using System.Collections;

public class WorldMapScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show() {
		gameObject.SetActive(true);
	}
	
	public void Hide() {
		gameObject.SetActive(false);
	}
}
