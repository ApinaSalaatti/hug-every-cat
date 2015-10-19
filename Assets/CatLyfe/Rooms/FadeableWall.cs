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
		if(Time.timeScale > 0f) {
			StartCoroutine(FadeColorIn());
		}
		else {
			// If paused, we want to change INSTANTLY!
			Color c = mRenderer.material.color;
			c.a = 1f;
			for(int i = 0; i < mRenderer.materials.Length; i++) {
				mRenderer.materials[i].color = c;
			}
		}
	}
	public void FadeOut() {
		CancelInvoke();
		StartCoroutine(FadeColorOut());
	}

	private IEnumerator FadeColorIn() {
		Color c = mRenderer.material.color;
		while(c.a < 1f) {
			for(int i = 0; i < mRenderer.materials.Length; i++) {
				mRenderer.materials[i].color = c;
			}
			c.a += Time.deltaTime;
			yield return null;
		}
	}

	private IEnumerator FadeColorOut() {
		Color c = mRenderer.material.color;
		while(c.a > 0.01f) {
			for(int i = 0; i < mRenderer.materials.Length; i++) {
				mRenderer.materials[i].color = c;
			}
			c.a -= Time.deltaTime;
			yield return null;
		}
	}
}
