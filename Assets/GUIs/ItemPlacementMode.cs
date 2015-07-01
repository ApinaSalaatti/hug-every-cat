using UnityEngine;
using System.Collections;

public class ItemPlacementMode : MonoBehaviour {
	private static ItemPlacementMode instance;
	public static ItemPlacementMode Instance { get { return instance; } }

	// =============================================================================

	public delegate void PlacementEndListener();

	public PlacementEndListener confirmListeners;
	public PlacementEndListener cancelListeners;

	private bool running = false;

	private GameObject itemToPlace;

	// Use this for initialization
	void Awake() {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(running) {
			Vector3 pos = PlayerInputManager.GetMouseWorldPosition();
			itemToPlace.transform.position = pos;
		}
	}

	public void Confirm() {
		confirmListeners();
		End();
	}
	public void Cancel() {
		cancelListeners();
		End();
	}

	public void Begin(GameObject item) {
		if(item != null) {
			running = true;
			transform.localScale = new Vector3(1f, 1f, 1f);
			itemToPlace = item;
		}
		else {
			Cancel();
		}
	}

	// Private because must happen via confirm or cancel
	private void End() {
		running = false;
		itemToPlace = null;
		transform.localScale = Vector3.zero;
	}
}
