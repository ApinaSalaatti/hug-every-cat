using UnityEngine;
using System.Collections;

public class PlayerInputManager : MonoBehaviour {
	private GameObject currentlyClicked;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P)) {
			if(WorldUpdate.Paused)
				WorldUpdate.PauseGame();
			else
				WorldUpdate.UnpauseGame();
		}
	}

	public void OnPointerDown() {
		Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		int layerMask = LayerMask.GetMask("Clickable");

		Collider2D col = Physics2D.OverlapPoint(point, layerMask, -5, 5);
		if(col != null) {
			currentlyClicked = col.gameObject;
		}
	}
	public void OnPointerUp() {
		if(currentlyClicked != null) {
			Debug.Log(currentlyClicked.name);
		}
	}
}
