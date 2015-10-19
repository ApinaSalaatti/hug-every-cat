using UnityEngine;
using System.Collections;

public class WorldBackground : MonoBehaviour {
	private static WorldBackground instance;
	public static WorldBackground Instance { get { return instance; } }
	[SerializeField]
	private Camera[] affectedCameras;
	[SerializeField]
	private GameObject[] swirls;

	private Color normalColor;
	private Color targetColor;

	private bool coloring = false;

	[SerializeField]
	private Color[] colors;
	private int currentColor = 0;

	private float timer = 0f;

	// Use this for initialization
	void Awake() {
		instance = this;

		normalColor = affectedCameras[0].backgroundColor;
		StopColors();
	}

	
	// Update is called once per frame
	void Update () {
		if(coloring) {
			for(int i = 0; i < affectedCameras.Length; i++) {
				affectedCameras[i].backgroundColor = Color.Lerp(affectedCameras[i].backgroundColor, colors[currentColor], Time.deltaTime / 5f);
			}
			timer += Time.deltaTime;
			if(timer >= 5f) {
				timer = 0f;
				currentColor++;
				currentColor %= colors.Length;
			}
		}
		else {
			for(int i = 0; i < affectedCameras.Length; i++) {
				affectedCameras[i].backgroundColor = Color.Lerp(affectedCameras[i].backgroundColor, normalColor, Time.deltaTime / 5f);
			}
		}
	}

	public void StartColors() {
		for(int i = 0; i < swirls.Length; i++) {
			swirls[i].SetActive(true);
		}
		coloring = true;
		timer = 0f;
	}
	public void StopColors() {
		for(int i = 0; i < swirls.Length; i++) {
			swirls[i].SetActive(false);
		}
		coloring = false;
	}
}
