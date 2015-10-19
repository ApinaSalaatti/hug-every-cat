using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerTrigger))]
public class Room : MonoBehaviour {
	[SerializeField]
	private FadeableWall[] walls;

	[SerializeField]
	private Transform handPosition;
	[SerializeField]
	private Door mainDoor;

	[SerializeField]
	private float requiredNoise = 50f;
	public float RequiredNoise { get { return requiredNoise; } }

	private float noiseAmount = 0f;
	public float CurrentNoise { get { return noiseAmount; } }
	private bool requiredNoiseAcquired = false;

	// Update is called once per frame
	void Update () {
	
	}

	// Called by HumanAlertness for the room the player is currently in
	public void NoiseHeard(float noiseLevel) {
		noiseAmount += noiseLevel;
		if(!requiredNoiseAcquired && noiseAmount >= requiredNoise) {
			requiredNoiseAcquired = true;
			if(mainDoor != null) // No door means no openings
				mainDoor.Open();
			if(handPosition != null) // Null handPosition means no hand!
				HumanAlertness.Instance.SpawnHand(handPosition.position);
		}
	}

	private void PlayerEnteredVicinity(GameObject player) {
		HumanAlertness.Instance.SetCurrentRoom(this);
		RoomManager.Instance.EnterRoom(this);

	}
	private void PlayerExitedVicinity(GameObject player) {

	}

	public void FadeIn() {
		for(int i = 0; i < walls.Length; i++) {
			walls[i].FadeIn();
		}
	}
	public void FadeOut() {
		for(int i = 0; i < walls.Length; i++) {
			walls[i].FadeOut();
		}
	}

	public void Appear() {
		MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
		for(int i = 0; i < renderers.Length; i++) {
			renderers[i].enabled = true;
		}
	}
	public void Disappear() {
		MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
		for(int i = 0; i < renderers.Length; i++) {
			renderers[i].enabled = false;
		}
	}

	public void SetAlpha(float a) {
		MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
		for(int i = 0; i < renderers.Length; i++) {
			Color c = renderers[i].material.color;
			c.a = a;
			renderers[i].material.color = c;
		}
	}
}
