using UnityEngine;
using System.Collections;

public class TextureChanger : MonoBehaviour {
	[SerializeField]
	private Texture[] textures;
	[SerializeField]
	private float textureChangeInterval = 0.5f;
	[SerializeField]
	private bool randomOrder = false;

	private float timer = 0f;
	private int index = -1;
	private Renderer r;

	// Use this for initialization
	void Start () {
		r = GetComponent<Renderer>();
		ChangeTexture();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= textureChangeInterval) {
			timer = 0f;
			ChangeTexture();
		}
	}

	private void ChangeTexture() {
		if(randomOrder) {
			index = Random.Range(0, textures.Length);
		}
		else {
			index++;
			index %= textures.Length;
		}

		r.material.mainTexture = textures[index];
	}
}
