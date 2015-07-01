using UnityEngine;
using System.Collections;

// A MonoBehaviour that makes sprites that are higher on the y-axel be further "back" in the world, kind of simulating a third dimension
[ExecuteInEditMode]
public class IsometricSprite : MonoBehaviour {
	[SerializeField]
	private float bottomOffsetFromPosition;
	[SerializeField]
	private int absoluteOffset = 0;
	[SerializeField]
	private bool applyToChildren;

	private SpriteRenderer sprite;
	private SpriteRenderer[] childSprites;

	// Use this for initialization
	void Awake() {
		sprite = GetComponent<SpriteRenderer>();

		if(applyToChildren) {
			childSprites = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//int order = -(int)((transform.position.y - bottomOffsetFromPosition) * 100) + absoluteOffset; // A calculation I just made up. Seems to work.
		int order = -(int)(sprite.bounds.min.y * 100f) + absoluteOffset;

		sprite.sortingOrder = order;

		if(applyToChildren) {
			foreach(SpriteRenderer sr in childSprites) {
				sr.sortingOrder = order;
			}
		}
	}
}
