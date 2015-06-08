using UnityEngine;
using System.Collections;

public class SpriteUtilities : MonoBehaviour {
	// Returns a copy of the given sprite (i.e. a new sprite with a new texture with copied pixel data)
	public static Sprite CopySpriteData(Sprite s) {
		Texture2D tex = new Texture2D(s.texture.width, s.texture.height, s.texture.format, false);
		tex.filterMode = FilterMode.Point;
		tex.SetPixels(s.texture.GetPixels());
		tex.Apply();
		return Sprite.Create(tex, s.rect, new Vector2(0.5f, 0.5f), s.pixelsPerUnit, 1, SpriteMeshType.Tight);
	}
}
