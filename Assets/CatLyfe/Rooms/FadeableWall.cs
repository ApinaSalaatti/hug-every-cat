using UnityEngine;
using System.Collections;

public class FadeableWall : MonoBehaviour {
	private MeshRenderer mRenderer;

	// Use this for initialization
	void Start () {
		mRenderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FadeIn() {
		CancelInvoke();
		StartCoroutine(FadeColorIn());
	}
	public void FadeOut() {
		CancelInvoke();
		StartCoroutine(FadeColorOut());
	}

	private IEnumerator FadeColorIn() {
		Color c = mRenderer.material.color;
		while(c.a < 1f) {
			mRenderer.material.color = c;
			c.a += Time.deltaTime;
			yield return null;
		}
	}

	private IEnumerator FadeColorOut() {
		Color c = mRenderer.material.color;
		while(c.a > 0.01f) {
			mRenderer.material.color = c;
			c.a -= Time.deltaTime;
			yield return null;
		}
	}
}
