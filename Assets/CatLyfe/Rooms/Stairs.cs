using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour {
	[SerializeField]
	private Floor upperFloor;

	[SerializeField]
	private float upperFloorY;
	[SerializeField]
	private float lowerFloorY;
	
	private GameObject trackedPlayer;
	private bool playerOnStairs = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(playerOnStairs) {
			float playerY = trackedPlayer.transform.position.y - 0.5f;
			float percentUp = playerY / (upperFloorY - lowerFloorY);
			upperFloor.SetAlpha(percentUp);
		}
	}

	void PlayerEnteredVicinity(GameObject player) {
		trackedPlayer = player;
		upperFloor.Appear();
		playerOnStairs = true;
	}

	void PlayerExitedVicinity(GameObject player) {
		playerOnStairs = false;
		if((player.transform.position.y - 0.5f) < upperFloorY) {
			// Player is downstairs
			upperFloor.Disappear();
		}
		else {
			upperFloor.SetAlpha(1f);
		}
	}
}
