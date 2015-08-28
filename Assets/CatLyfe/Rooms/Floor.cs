using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	void Start() {
		Disappear();
	}

	public void Appear() {
		Room[] rooms = gameObject.GetComponentsInChildren<Room>();
		for(int i = 0; i < rooms.Length; i++) {
			rooms[i].Appear();
		}
	}
	public void Disappear() {
		Room[] rooms = gameObject.GetComponentsInChildren<Room>();
		for(int i = 0; i < rooms.Length; i++) {
			rooms[i].Disappear();
		}
	}

	public void SetAlpha(float a) {
		Room[] rooms = gameObject.GetComponentsInChildren<Room>();
		for(int i = 0; i < rooms.Length; i++) {
			rooms[i].SetAlpha(a);
		}
	}
}
