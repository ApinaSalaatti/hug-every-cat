using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalInput : MonoBehaviour {
	private static GlobalInput instance;
	public static GlobalInput Instance { get { return instance; } }

	[SerializeField]
	private GameObject pauseMenu;

	[SerializeField]
	private GameObject mainGUI;

	[SerializeField]
	private GameObject player;
	private PlayerMover mover;

	[SerializeField]
	private GameObject mainCamera;
	[SerializeField]
	private GameObject photoCamera;
	[SerializeField]
	private GameObject firstPersonCamera;
	[SerializeField]
	private GameObject background;

	private bool paused = false;

	private List<InputReceiver> inputReceivers;

	// Use this for initialization
	void Awake() {
		instance = this;

		inputReceivers = new List<InputReceiver>();

		mover = player.GetComponent<PlayerMover>();
	}
	
	// Update is called once per frame
	void Update () {
		// Loop from last to first so the lates addition is handled first
		for(int i = inputReceivers.Count-1; i >= 0; i++) {
			if(inputReceivers[i].ReceiveInput()) {
				return;
			}
		}

		mover.ReceiveInput();

		if(Input.GetButtonDown("Pause")) {
			// Game can only be unpaused from the menu
			if(!paused) {
				Pause();
			}
		}

		if(!paused && Input.GetButtonDown("CameraChange")) {
			//ToggleCamera();
		}
	}

	private bool firstPerson = false;
	private void ToggleCamera() {
		firstPerson = !firstPerson;
		player.SendMessage("ToggleFirstPerson", firstPerson);
		if(firstPerson) {
			RoomManager.Instance.ForceWalls(true);
			mainCamera.SetActive(false);
			firstPersonCamera.SetActive(true);
		}
		else {
			RoomManager.Instance.ForceWalls(false);
			firstPersonCamera.SetActive(false);
			mainCamera.SetActive(true);
		}
	}

	public void AddInputReceiver(InputReceiver ir) {
		inputReceivers.Add(ir);
	}
	public void RemoveInputReceiver(InputReceiver ir) {
		inputReceivers.Remove(ir);
	}

	public void Pause() {
		paused = true;
		Time.timeScale = 0f;
		pauseMenu.transform.localScale = new Vector3(1f, 1f, 1f);
		mainGUI.transform.localScale = Vector3.zero;

		player.GetComponent<PlayerMover>().enabled = false;

		Vector3 fromPlayer = mainCamera.transform.position - player.transform.position;
		fromPlayer = fromPlayer.normalized * 5f;
		photoCamera.transform.position = player.transform.position + fromPlayer;
		photoCamera.transform.rotation = mainCamera.transform.rotation;

		background.transform.SetParent(photoCamera.transform, true);

		RoomManager.Instance.ForceWalls(true);
		mainCamera.SetActive(false);
		firstPersonCamera.SetActive(false);
		photoCamera.SetActive(true);
	}
	public void Unpause() {
		paused = false;
		Time.timeScale = 1f;
		pauseMenu.transform.localScale = Vector3.zero;
		mainGUI.transform.localScale = new Vector3(1f, 1f, 1f);

		player.GetComponent<PlayerMover>().enabled = true;

		photoCamera.transform.position = mainCamera.transform.position;
		photoCamera.transform.rotation = mainCamera.transform.rotation;

		background.transform.SetParent(mainCamera.transform, true);

		photoCamera.SetActive(false);
		if(firstPerson) {
			firstPersonCamera.SetActive(true);
		}
		else {
			RoomManager.Instance.ForceWalls(false);
			mainCamera.SetActive(true);
		}
	}
}
