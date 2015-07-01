using UnityEngine;
using System.Collections;

public class HomeGUI : MonoBehaviour {
	private static HomeGUI instance;
	public static HomeGUI Instance { get { return instance; } }

	// Use this for initialization
	void Awake() {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Cancel")) {
			PauseMenu.Instance.Show();
		}
	}

	public void Show() {
		transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void Hide() {
		transform.localScale = Vector3.zero;
	}
}
