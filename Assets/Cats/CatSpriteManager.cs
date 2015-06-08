using UnityEngine;
using System.Collections;

public class CatSpriteManager : MonoBehaviour {
	[SerializeField]
	private GameObject spriteObject;
	public GameObject SpriteObject { get { return spriteObject; } }

	[SerializeField]
	private GameObject legObject;
	[SerializeField]
	private GameObject pawLeftObject;
	[SerializeField]
	private GameObject pawRightObject;

	private Sprite fullSprite; // For nice presentation purposes

	private SpriteRenderer spriteRenderer;

	void Awake() {
		spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
	}

	public void SetSprite(Sprite s) {
		fullSprite = CopySprite(s);
		Sprite body = CopySprite(s);

		// Create copies of the sprites so we don't modify the actual asset (I'm not really sure why that happens)
		legObject.GetComponent<SpriteRenderer>().sprite = CopySprite(legObject.GetComponent<SpriteRenderer>().sprite);
		pawLeftObject.GetComponent<SpriteRenderer>().sprite = CopySprite(pawLeftObject.GetComponent<SpriteRenderer>().sprite);
		pawRightObject.GetComponent<SpriteRenderer>().sprite = CopySprite(pawRightObject.GetComponent<SpriteRenderer>().sprite);

		SetEyes(body);

		spriteRenderer.sprite = body;
		SetLegsAndPaws(body);
	}
	public Sprite GetSprite() {
		return fullSprite;
	}

	private void SetEyes(Sprite s) {
		s.texture.SetPixel(23, 24, s.texture.GetPixel(23, 25));
		s.texture.SetPixel(27, 24, s.texture.GetPixel(27, 25));
		s.texture.Apply();
	}

	// A very hacky way of figuring out if a pixel is on a leg part of a texture. Simply checks if the pixel to the left and right are transparent
	private bool IsLeg(int x, int y, Texture2D t) {
		if(x <= 0 || x >= t.width-1) {
			return false;
		}

		Color left = t.GetPixel(x-1, y);
		Color right = t.GetPixel(x+1, y);
		return left.a == 0f && right.a == 0f;
	}

	private Sprite CopySprite(Sprite s) {
		Texture2D tex = new Texture2D(s.texture.width, s.texture.height, s.texture.format, false);
		tex.filterMode = FilterMode.Point;
		tex.SetPixels(s.texture.GetPixels());
		tex.Apply();
		return Sprite.Create(tex, s.rect, new Vector2(0.5f, 0.5f), s.pixelsPerUnit, 1, SpriteMeshType.Tight);
	}

	private void SetLegsAndPaws(Sprite s) {
		int w = s.texture.width;
		int h = s.texture.height;
		Sprite legSprite = legObject.GetComponent<SpriteRenderer>().sprite;
		Sprite pawLeftSprite = pawLeftObject.GetComponent<SpriteRenderer>().sprite;
		Sprite pawRightSprite = pawRightObject.GetComponent<SpriteRenderer>().sprite;

		for(int y = 0; y < h; y++) {
			for(int x = 0; x < w; x++) {
				if(legSprite.texture.GetPixel(x, y).a == 1f) {
					if(IsLeg(x, y, s.texture))
						TransferPixel(x, y, s, legSprite);
					else
						legSprite.texture.SetPixel(x, y, s.texture.GetPixel(x, y));
				}
				if(pawLeftSprite.texture.GetPixel(x, y).a == 1f) {
					TransferPixel(x, y, s, pawLeftSprite);
				}
				if(pawRightSprite.texture.GetPixel(x, y).a == 1f) {
					TransferPixel(x, y, s , pawRightSprite);
				}
			}
		}
		legSprite.texture.Apply();
		pawLeftSprite.texture.Apply();
		pawRightSprite.texture.Apply();
		s.texture.Apply();
	}

	private void TransferPixel(int x, int y, Sprite from, Sprite to) {
		Color c = from.texture.GetPixel(x, y);
		to.texture.SetPixel(x, y, c);
		from.texture.SetPixel(x, y, new Color(1f, 1f, 1f, 0f));
	}
}
