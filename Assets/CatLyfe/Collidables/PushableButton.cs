using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CollidableObject))]
public class PushableButton : MonoBehaviour {
	[SerializeField]
	private Light light;

	[SerializeField]
	private GameObject[] listeners;

	private bool pushed = false;

	void OnCollisionWithPlayer(GameObject player) {
		if(pushed)
			return;

		light.enabled = true;

		pushed = true;
		for(int i = 0; i < listeners.Length; i++) {
			listeners[i].SendMessage("OnButtonPushed", this);
		}
	}
}
