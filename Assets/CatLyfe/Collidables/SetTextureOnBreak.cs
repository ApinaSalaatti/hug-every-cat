using UnityEngine;
using System.Collections;

public class SetTextureOnBreak : MonoBehaviour {
	[SerializeField]
	private Renderer rendererToAffect;
	[SerializeField]
	private Texture textureToSet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnBreak(GameObject player) {
		if(rendererToAffect != null)
			rendererToAffect.material.mainTexture = textureToSet;
		else
			GetComponent<Renderer>().material.mainTexture = textureToSet;
	}
}
