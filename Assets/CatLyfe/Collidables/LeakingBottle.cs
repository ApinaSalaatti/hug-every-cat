using UnityEngine;
using System.Collections;

public class LeakingBottle : MonoBehaviour {
	[SerializeField]
	private GameObject spillPrefab;

	private bool leaking = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col) {
		GameObject g = Instantiate(spillPrefab, col.contacts[0].point, Quaternion.FromToRotation(Vector3.back, col.contacts[0].normal)) as GameObject;
		float r = Random.value + 0.5f;
		g.transform.localScale = new Vector3(r, r, r);
	}

	void OnBreak(GameObject player) {
		leaking = true;
	}
}
