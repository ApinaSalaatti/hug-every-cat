using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatSelectButton : MonoBehaviour {
	[SerializeField]
	private Image image;
	[SerializeField]
	private Text text;

	public void SetCat(CatExportImportData cat) {
		Sprite s = Sprite.Create(cat.texture, new Rect(0f, 0f, 32f, 32f), new Vector2(0.5f, 0.5f), 32);
		image.sprite = s;
		if(cat.name != null && cat.name != "") {
			text.text = cat.name;
		}
		else {
			cat.name = "Unnamed";
			text.text = "Unnamed";
		}
	}
}
