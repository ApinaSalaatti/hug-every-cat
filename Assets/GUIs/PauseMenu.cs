using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	private static PauseMenu instance;
	public static PauseMenu Instance { get { return instance; } }

	// Use this for initialization
	void Awake() {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void QuitGame() {
		Application.LoadLevel(0);
	}

	public void Show() {
		transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void Hide() {
		transform.localScale = Vector3.zero;
	}
}
