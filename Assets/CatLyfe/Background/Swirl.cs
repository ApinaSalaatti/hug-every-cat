using UnityEngine;
using System.Collections;

public class Swirl : MonoBehaviour {
	private float speed;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.SetColor("_TintColor", new Color(Random.value, Random.value, Random.value, Random.Range(0.5f, 0.8f)));

		float s = Random.Range(100f, 200f);
		transform.localScale = new Vector3(s, s, s);

		speed = Random.Range(-20f, 20f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(transform.position, transform.forward, speed * Time.deltaTime);
	}
}
