using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CenterOnObject(GameObject obj) {
		Vector3 pos = obj.transform.position;
		pos.z = transform.position.z;
		transform.position = pos;
	}
}
