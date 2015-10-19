using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(SplashSequence());
	}

	private IEnumerator SplashSequence() {
		yield return new WaitForSeconds(1f);
		Application.LoadLevel(1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
