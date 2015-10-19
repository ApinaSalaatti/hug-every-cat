using UnityEngine;
using System.Collections;

public class CameraEffects : MonoBehaviour {
	private float rumbleTime = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(rumbleTime > 0f) {
			Debug.Log("RUMBLINGGGG");
			rumbleTime -= Time.deltaTime;
			Vector2 displacement = Random.insideUnitCircle.normalized / 5f;
			Vector3 pos = transform.position;
			pos.x += displacement.x;
			pos.y += displacement.y;
			transform.position = pos;
		}
	}

	public void Rumble(float time) {
		rumbleTime = time;
	}
}
