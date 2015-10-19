using UnityEngine;
using System.Collections;

public class Glow : MonoBehaviour {
	private Vector3 baseScale;

	// Use this for initialization
	void Start () {
		baseScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		float addition = Mathf.Abs(Mathf.Sin(Time.time * 5f)) / 10f;
		Vector3 s = new Vector3(baseScale.x + addition, baseScale.y + addition, baseScale.z + addition);
		transform.localScale = s;
	}
}
