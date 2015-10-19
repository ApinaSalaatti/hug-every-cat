using UnityEngine;
using System.Collections;

public class TextureRandomizer : MonoBehaviour {
	[SerializeField]
	private Texture[] texturesToChooseFrom;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.mainTexture = texturesToChooseFrom[Random.Range(0, texturesToChooseFrom.Length)];
	}
}
