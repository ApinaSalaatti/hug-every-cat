using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorSelector : MonoBehaviour {
	private Color color;
	public Color Color {
		get { return color; }
	}

	[SerializeField]
	private Image preview;
	[SerializeField]
	private ColorTool colorTool;

	void Awake() {
		color = new Color(1f, 1f, 1f, 1f);
	}

	void Update() {
		color.r = colorTool.rSlider.value;
		color.g = colorTool.gSlider.value;
		color.b = colorTool.bSlider.value;

		preview.color = color;
	}

	public void Randomize() {
		colorTool.rSlider.value = Random.value;
		colorTool.gSlider.value = Random.value;
		colorTool.bSlider.value = Random.value;
	}
	public void SetColor(float r, float g, float b) {
		colorTool.rSlider.value = r;
		colorTool.gSlider.value = g;
		colorTool.bSlider.value = b;
	}

	private bool open = false;
	public void Open() {
		transform.localScale = new Vector3(1f, 1f, 1f);
		open = true;
	}

	public void Close() {
		transform.localScale = Vector3.zero;
		open = false;
	}

	public void Toggle() {
		if(open)
			Close();
		else
			Open();
	}
}
