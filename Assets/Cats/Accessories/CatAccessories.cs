using UnityEngine;
using System.Collections;

public class CatAccessories : MonoBehaviour {
	[SerializeField]
	private GameObject hatObject;

	// Use this for initialization
	void Start () {
	
	}

	public void SetHat(Sprite h) {
		hatObject.GetComponent<SpriteRenderer>().sprite = h;
	}
	public void SetHat(string h) {
		hatObject.GetComponent<SpriteRenderer>().sprite = CatAccessoryManager.Instance.GetHat(h);
	}
}
