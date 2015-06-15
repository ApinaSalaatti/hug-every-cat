using UnityEngine;
using System.Collections;

public class ShopScreen : MonoBehaviour {
	private static ShopScreen instance;
	public static ShopScreen Instance { get { return instance; } }

	/*
	 * Instance stuff from here
	 */
	private bool screenShown = false;

	// Use this for initialization
	void Awake() {
		instance = this;
	}

	public void Show() {
		transform.localScale = new Vector3(1f, 1f, 1f);
		screenShown = true;
	}
	public void Hide() {
		transform.localScale = Vector3.zero;
		screenShown = false;
	}
}
