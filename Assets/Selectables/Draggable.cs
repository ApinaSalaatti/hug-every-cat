using UnityEngine;
using System.Collections;

public class Draggable : MonoBehaviour {

	void OnSelect() {
		Debug.Log("SELECTED!!");
	}

	void OnDeselect() {
		Debug.Log("Deselecteddddd!");
	}

	void OnDrag() {
		Vector3 pos = PlayerInputManager.GetMouseWorldPosition();
		pos.z = transform.position.z;
		transform.position = pos;
	}
}
