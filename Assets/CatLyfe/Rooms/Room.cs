using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerTrigger))]
public class Room : MonoBehaviour {
	[SerializeField]
	private FadeableWall[] walls;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void PlayerEnteredVicinity(GameObject player) {
		Debug.Log(player.name + " entered room: " + gameObject.name);
		for(int i = 0; i < walls.Length; i++) {
			walls[i].FadeOut();
		}
	}
	private void PlayerExitedVicinity(GameObject player) {
		Debug.Log(player.name + " entered room: " + gameObject.name);
		for(int i = 0; i < walls.Length; i++) {
			walls[i].FadeIn();
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
