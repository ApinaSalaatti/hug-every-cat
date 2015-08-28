using UnityEngine;
using System.Collections;

public class PauseGUI : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ExitGame() {
		ConfirmDialog.Instance.Show("Are you really sure you wish to quit, meow?", DoExit);
	}

	private void DoExit() {
		Application.LoadLevel(0);
	}
}
