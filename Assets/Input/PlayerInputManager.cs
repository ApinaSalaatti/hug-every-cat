using UnityEngine;
using System.Collections;

public class PlayerInputManager : MonoBehaviour {
	private GameObject beingSelected;
	private GameObject selected;

	private Vector2 lastMousePos;
	private bool pointerDown = false;
	private bool dragged = false;

	private bool movingObject = false;
	private bool movingCamera = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Check for mouse drag
		if(pointerDown) {
			Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if(Vector2.Distance(currentMousePos, lastMousePos) > 0.01f) {
				Vector2 diff = currentMousePos - lastMousePos;
				dragged = true;
				if(selected != null && selected == beingSelected) { // We get here when we first select an object, then push the pointer again on top of it and start to drag
					selected.SendMessage("OnDrag", SendMessageOptions.DontRequireReceiver);
				}
				else {
					// Move camera
					Vector3 cameraPos = Camera.main.transform.position;
					cameraPos.x += diff.x * -1;
					cameraPos.y += diff.y * -1f;
					Camera.main.transform.position = cameraPos;
					currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Reset current mouse position because the camera moved
				}
			}
			lastMousePos = currentMousePos;
		}

		if(Input.GetKeyDown(KeyCode.P)) {
			if(WorldUpdate.Paused)
				WorldUpdate.PauseGame();
			else
				WorldUpdate.UnpauseGame();
		}
	}

	private GameObject GetObjectUnderMouse() {
		Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		int layerMask = LayerMask.GetMask("Cat", "House Item");
		
		Collider2D col = Physics2D.OverlapPoint(point, layerMask, -5, 5);
		if(col != null) {
			return col.gameObject;
		}
		return null;
	}

	public void OnPointerDown() {
		lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		beingSelected = GetObjectUnderMouse();
		pointerDown = true;

		// Reset these
		dragged = false;
		movingCamera = false;
		movingObject = false;
	}
	public void OnPointerUp() {
		pointerDown = false;
		GameObject nowUnderMouse = GetObjectUnderMouse();
		if(beingSelected != null && !dragged) { // Must be the same object that we started to click
			if(selected != null) selected.SendMessage("OnDeselect", SendMessageOptions.DontRequireReceiver);
			beingSelected.SendMessage("OnSelect", SendMessageOptions.DontRequireReceiver);
			selected = beingSelected;
			beingSelected = null;
			//Debug.Log("SELECTED : " + selected.name);
		}
		else if(beingSelected == null && !dragged && selected != null) {
			selected.SendMessage("OnDeselect", SendMessageOptions.DontRequireReceiver);
			selected = null;
		}
	}

	// Simple helper method to get the mouse's world position
	public static Vector3 GetMouseWorldPosition() {
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
