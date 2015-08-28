using UnityEngine;
using System.Collections;

public class PreviewCatSpinner : MonoBehaviour {
	private Transform trans;

	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 rot = trans.rotation.eulerAngles;
		rot.y += Time.deltaTime * 15f;
		trans.rotation = Quaternion.Euler(rot);
	}
}
