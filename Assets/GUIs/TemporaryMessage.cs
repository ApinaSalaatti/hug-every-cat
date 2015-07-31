using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TemporaryMessage : MonoBehaviour {
	private Text text;
	private float currentAlpha = 0f;
	private float alphaFadeRatio = 0f;
	private bool showing = false;
	private bool destroy = false;

	// Use this for initialization
	void Awake() {
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(showing) {
			currentAlpha -= alphaFadeRatio * Time.deltaTime;
			Color c = text.color;
			c.a = currentAlpha;
			text.color = c;
			if(destroy && currentAlpha < 0f) {
				Destroy(gameObject);
			}
		}
	}

	public void Show(string m, float time, bool destroyWhenDone = false) {
		text.text = m;
		currentAlpha = 2f;
		alphaFadeRatio = 2f / time;
		destroyWhenDone = destroy;

		showing = true;
	}
}
