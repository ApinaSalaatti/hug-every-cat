using UnityEngine;
using System.Collections;

public class SetRandomFur : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Renderer[] r = GetComponentsInChildren<Renderer>();
		Texture tex = CatFactory.Instance.CreateRandomTexture();
		for(int i = 0; i < r.Length; i++)
			r[i].material.mainTexture = tex;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
