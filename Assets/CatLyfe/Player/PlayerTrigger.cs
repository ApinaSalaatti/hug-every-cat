using UnityEngine;
using System.Collections;

// Calls given listeners when player enters and exits the trigger
public class PlayerTrigger : MonoBehaviour {
	[SerializeField]
	private GameObject[] listeners;

	private int playerLayer;

	// Use this for initialization
	void Start () {
		playerLayer = LayerMask.NameToLayer("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if(col.gameObject.layer == playerLayer) {
			gameObject.SendMessage("PlayerEnteredVicinity", col.gameObject, SendMessageOptions.DontRequireReceiver);

			for(int i = 0; i < listeners.Length; i++) {
				listeners[i].SendMessage("PlayerEnteredVicinity", col.gameObject);
			}
		}
	}
	void OnTriggerExit(Collider col) {
		if(col.gameObject.layer == playerLayer) {
			gameObject.SendMessage("PlayerExitedVicinity", col.gameObject, SendMessageOptions.DontRequireReceiver);

			for(int i = 0; i < listeners.Length; i++) {
				listeners[i].SendMessage("PlayerExitedVicinity", col.gameObject);
			}
		}
	}
}
