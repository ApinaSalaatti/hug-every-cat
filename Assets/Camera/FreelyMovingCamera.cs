﻿using UnityEngine;
using System.Collections;

public class FreelyMovingCamera : MonoBehaviour, InputReceiver {
	[SerializeField]
	private GameObject player;

	private Vector2 mouseDownPos;
	private Vector2 lastMousePos;
	private bool pointerDown = false;
	private bool dragged = false;

	private bool turning = false;

	private Camera camera;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	public bool ReceiveInput() {
		Vector3 movement = new Vector3(0f, 0f, 0f);
		if(pointerDown) {
			Vector2 currentMousePos = Input.mousePosition;
			if(Vector2.Distance(currentMousePos, mouseDownPos) > 0.1f) {
				Vector2 diff = currentMousePos - lastMousePos;
				dragged = true;

				if(turning) {
					// If turning, we don't modify the movement vector but rotate the camera
					transform.RotateAround(transform.position, Vector3.up, diff.x);
					//transform.RotateAround(transform.position, Vector3.right, diff.y);
				}
				else {
					// Else use the movement to move the camera
					movement.x = -diff.x/3f;
					movement.y = -diff.y/3f;
				}

				currentMousePos = Input.mousePosition; // Reset current mouse position because the camera moved
			}
			lastMousePos = currentMousePos;
		}
		else if(Input.GetAxis("Mouse ScrollWheel") != 0f) {
			movement.z = Input.GetAxis("Mouse ScrollWheel") * 5f;
		}
		else {
			movement.x = Input.GetAxisRaw("Horizontal");
			movement.z = Input.GetAxisRaw("Vertical");
		}

		movement = transform.TransformDirection(movement);

		// Move camera
		Vector3 cameraPos = transform.position;
		cameraPos += movement;
		if(cameraPos.y < player.transform.position.y+1f) {
			cameraPos.y = player.transform.position.y+1f;
		}

		if(Vector3.Distance(cameraPos, player.transform.position) < 30f) {
			transform.position = cameraPos;
		}

		return true;
	}

	public void PointerDown() {
		Debug.Log("Clickin");

		if (Input.GetMouseButtonDown(1))
			turning = true;
		else
			turning = false;

		lastMousePos = Input.mousePosition;
		mouseDownPos = lastMousePos;
		//Debug.Log(beingSelected);
		pointerDown = true;
		
		// Reset these
		dragged = false;
	}
	public void PointerUp() {
		Debug.Log("stop clickin");
		pointerDown = false;
	}

	void OnEnable() {
		GlobalInput.Instance.AddInputReceiver(this);
	}
	void OnDisable() {
		GlobalInput.Instance.RemoveInputReceiver(this);
	}
}
