using UnityEngine;
using System.Collections;

[System.Serializable]
public class CatBodyData {
	public Sprite emptyCat;
	public Sprite legalPixels;
}

public class CatImages : MonoBehaviour {
	[SerializeField]
	private CatBodyData skinnyCat;
	public CatBodyData Skinny { get { return skinnyCat; } }

	[SerializeField]
	private CatBodyData mediumCat;
	public CatBodyData Medium { get { return mediumCat; } }

	[SerializeField]
	private CatBodyData fatCat;
	public CatBodyData Fat { get { return fatCat; } }

	// Returns a copy of a Sprite of a given body type
	public Sprite GetNewSprite(BodyType bt) {
		Sprite orig = null;
		switch(bt) {
		case BodyType.SKINNY:
			orig = skinnyCat.emptyCat;
			break;
		case BodyType.MEDIUM:
			orig = mediumCat.emptyCat;
			break;
		case BodyType.FAT:
			orig = fatCat.emptyCat;
			break;
		}

		return SpriteUtilities.CopySpriteData(orig);
	}
	public Sprite GetLegalPixels(BodyType bt) {
		switch(bt) {
		case BodyType.SKINNY:
			return skinnyCat.legalPixels;
		case BodyType.MEDIUM:
			return mediumCat.legalPixels;
		case BodyType.FAT:
			return fatCat.legalPixels;
		default:
			return mediumCat.legalPixels;
		}
	}

	public void ApplyFur(Sprite sprite, Color[,] fur, BodyType bt) {

	}

	// Returns true if the given pixel of the given body type
	public bool IsLegal(int x, int y, BodyType bt) {
		Sprite legalPixels = GetLegalPixels(bt);
		if(x < 0 || x >= legalPixels.texture.width || y < 0 || y >= legalPixels.texture.height)
			return false;
		
		return legalPixels.texture.GetPixel(x, y).a > 0f;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
