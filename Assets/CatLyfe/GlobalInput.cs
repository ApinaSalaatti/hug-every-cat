using UnityEngine;
using System.Collections;

public class GlobalInput : MonoBehaviour {
	[SerializeField]
	private GameObject pauseMenu;
	[SerializeField]
	private GameObject mainGUI;

	[SerializeField]
	private GameObject mainCamera;
	[SerializeField]
	private GameObject photoCamera;

	private bool paused = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Pause")) {
			// Game can only be unpaused from the menu
			if(!paused) {
				Pause();
			}
		}
	}

	public void Pause() {
		paused = true;
		Time.timeScale = 0f;
		pauseMenu.transform.localScale = new Vector3(1f, 1f, 1f);
		mainGUI.transform.localScale = Vector3.zero;

		mainCamera.SetActive(false);
		photoCamera.SetActive(true);
	}
	public void Unpause() {
		paused = false;
		Time.timeScale = 1f;
		pauseMenu.transform.localScale = Vector3.zero;
		mainGUI.transform.localScale = new Vector3(1f, 1f, 1f);

		mainCamera.SetActive(true);
		photoCamera.SetActive(false);
	}
}
