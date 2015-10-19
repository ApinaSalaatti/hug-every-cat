using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	private bool open = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Open() {
		open = true;
		GetComponent<Animator>().SetBool("Open", true);
	}

	public void Close() {
		open = false;
		GetComponent<Animator>().SetBool("Open", false);
	}

	// Listener for a button event
	void OnButtonPushed(PushableButton button) {
		if(open)
			Close();
		else
			Open();
	}
}
