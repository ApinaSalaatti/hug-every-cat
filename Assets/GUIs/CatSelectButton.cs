using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatSelectButton : MonoBehaviour {
	[SerializeField]
	private Image image;
	[SerializeField]
	private Text text;

	public void SetCat(GameObject cat) {
		image.sprite = cat.GetComponent<CatSpriteManager>().GetSprite();
		text.text = cat.GetComponent<CatStats>().Name;
	}
}
