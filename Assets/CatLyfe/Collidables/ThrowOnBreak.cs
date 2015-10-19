using UnityEngine;
using System.Collections;

public class ThrowOnBreak : MonoBehaviour {
	[SerializeField]
	private bool disableTrigger = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnBreak(GameObject player) {
		if(disableTrigger)
			GetComponent<Collider>().isTrigger = false;
		gameObject.AddComponent<Rigidbody>();
		Vector3 dir = (transform.position - player.transform.position);
		dir.y = 0;
		CharacterController ctrl = player.GetComponent<CharacterController>();
		if(ctrl.velocity.y > 0) {
			// If the player is moving up, add some force upwards. Otherwise the y-component is zero
			dir.y = 1f;
		}
		dir = dir.normalized;
		GetComponent<Rigidbody>().AddForce(dir * 500f);
	}
}
