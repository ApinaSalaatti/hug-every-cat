using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStart : MonoBehaviour {
	[SerializeField]
	private GameObject playerCat;

	[SerializeField]
	private InputField nameField;

	// Use this for initialization
	void Start() {
		CatExportImportData cat = CatFactory.Instance.CreateFromFile("startingCat");

		//Sprite s = cat.GetComponent<CatSpriteManager>().GetSprite();
		Renderer r = playerCat.GetComponentInChildren<Renderer>();
		r.sharedMaterial.mainTexture = cat.texture;

		CatStats s = playerCat.GetComponent<CatStats>();
		s.Name = cat.name;
		s.Gender = cat.gender;

		// TODO: maybe make something that actually makes sense rather than this hacky shit
		// NOTE TO FUTURE SELF WHO DOESN'T REMEMBER WHAT THIS IS:
		// It's the input field that's attached to the Catstagram send GUI because it requires an input field...
		nameField.text = cat.name;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
