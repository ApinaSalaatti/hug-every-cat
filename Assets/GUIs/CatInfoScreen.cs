using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatInfoScreen : MonoBehaviour {
	/*
	 * Global static stuff
	 */
	private static CatInfoScreen instance;
	
	public static CatInfoScreen Instance() {
		if(!instance) {
			instance = FindObjectOfType<CatInfoScreen>();
			if(!instance) {
				Debug.LogError("There's no CatInfoScreen to be found you silly cat turd!");
			}
		}
		return instance;
	}

	/*
	 * Instance stuff from here
	 */
	[SerializeField]
	private Text nameText;
	[SerializeField]
	private Image catImage;

	private bool screenShown = false;
	private GameObject shownCat;
	private CatStats catStats;

	public void Show(GameObject cat) {
		if(cat != null) {
			transform.localScale = new Vector3(1f, 1f, 1f);
			shownCat = cat;
			catStats = cat.GetComponent<CatStats>();
			screenShown = true;

			catImage.sprite = cat.GetComponent<CatSpriteManager>().GetSprite();
		}
	}
	public void Hide() {
		transform.localScale = Vector3.zero;
		screenShown = false;
	}

	void Update() {
		if(screenShown) {
			if(shownCat == null || catStats == null) {
				Hide();
				return;
			}

			nameText.text = catStats.Name;
		}
	}
}
