using UnityEngine;
using System.Collections;

public class ParticleSpawner : MonoBehaviour {
	private static ParticleSpawner instance;
	public static ParticleSpawner Instance { get { return instance; } }

	[SerializeField]
	private GameObject itemScoredParticlesPrefab;

	// Use this for initialization
	void Awake() {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnItemScoredParticles(Vector3 pos) {
		GameObject obj = Instantiate(itemScoredParticlesPrefab, pos, Quaternion.identity) as GameObject;
		Color c = new Color(Random.value, Random.value, Random.value);
		obj.GetComponent<ParticleSystem>().startColor = c;
	}
}
