using UnityEngine;
using System.Collections;

public class TextEffectSpawner : MonoBehaviour {
	private static TextEffectSpawner instance;
	public static TextEffectSpawner Instance { get { return instance; } }

	[SerializeField]
	private GameObject textEffectPrefab;

	// Use this for initialization
	void Awake() {
		instance = this;
	}

	public GameObject SpawnTextEffect(string text, Vector3 pos, float time = 1f) {
		GameObject obj = Instantiate(textEffectPrefab, pos, Quaternion.identity) as GameObject;
		obj.GetComponent<TextEffect>().ShowText(text, time);
		return obj;
	}
	public GameObject SpawnTextEffect(string text, Vector3 pos, Color col, float time = 1f) {
		GameObject obj = Instantiate(textEffectPrefab, pos, Quaternion.identity) as GameObject;
		obj.GetComponent<TextEffect>().ShowText(text, time, col);
		return obj;
	}
}
