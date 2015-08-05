using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorSelector : MonoBehaviour {
	private Color color;
	public Color Color {
		get { return color; }
	}

	[SerializeField]
	private Image colorDisplay;
	[SerializeField]
	private Image preview;
	[SerializeField]
	private ColorTool colorTool;

	[SerializeField]
	private Image pickerImage;

	void Awake() {
		color = new Color(1f, 1f, 1f, 1f);
	}

	void Update() {
		//color.r = colorTool.rSlider.value;
		//color.g = colorTool.gSlider.value;
		//color.b = colorTool.bSlider.value;

		//preview.color = color;

		if(open) {
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = pickerImage.transform.position.z;
			mousePos = pickerImage.transform.InverseTransformPoint(mousePos);
			
			// A correction because of the pivot point being at the center
			mousePos.x += 0.5f * pickerImage.sprite.texture.width;
			mousePos.y += 0.5f * pickerImage.sprite.texture.height;
			
			int x = Mathf.FloorToInt(mousePos.x);// * pickerImage.sprite.texture.width);
			int y = Mathf.FloorToInt(mousePos.y);// * pickerImage.sprite.texture.height);
			if(x > 0 && y > 0 && x < pickerImage.sprite.texture.width && y < pickerImage.sprite.texture.height) {
				Color c = pickerImage.sprite.texture.GetPixel(x, y);
				//Debug.Log(c);
				SetColor (c.r, c.g, c.b);
			}

			Color col = new Color(colorTool.rSlider.value, colorTool.gSlider.value, colorTool.bSlider.value);
			preview.color = col;
		}
	}

	public void SelectColor() {
		color.r = colorTool.rSlider.value;
		color.g = colorTool.gSlider.value;
		color.b = colorTool.bSlider.value;
		colorDisplay.color = color;
		Close();
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
