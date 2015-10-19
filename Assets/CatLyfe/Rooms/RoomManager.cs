using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour {
	private static RoomManager instance;
	public static RoomManager Instance { get { return instance; } }
	private Room currentRoom;
	public Room CurrentRoom { get { return currentRoom; } }

	void Awake() {
		instance = this;
	}

	public void EnterRoom(Room r) {
		if(!forceWalls) {
			if(currentRoom != null) {
				currentRoom.FadeIn();
			}
			currentRoom = r;
			currentRoom.FadeOut();
		}
	}

	private bool forceWalls = false;
	public void ForceWalls(bool force) {
		forceWalls = force;

		if(currentRoom == null)
			return;

		if(force) {
			currentRoom.FadeIn();
		}
		else {
			currentRoom.FadeOut();
		}
	}
}
