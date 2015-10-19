using UnityEngine;
using System.Collections;

public class FoodBowl : MonoBehaviour {
	[SerializeField]
	private GameObject foodPrefab;

	[SerializeField]
	private GameObject foodToDisable;

	private float timer = 0f;
	private float spawnTimer = 0f;
	private float eatTime;

	private bool eating = false;

	private bool alreadyEaten = false;

	// Use this for initialization
	void Start () {
		eatTime = GetComponent<PlayableObject>().PlayTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(eating) {
			timer += Time.deltaTime;
			spawnTimer += Time.deltaTime;

			if(spawnTimer >= 0.1f) {
				spawnTimer = 0f;
				GameObject food = Instantiate(foodPrefab, transform.position+Vector3.up, Quaternion.identity) as GameObject;
				food.GetComponent<Rigidbody>().AddForce(new Vector3(Random.value-0.5f, 1f, Random.value-0.5f) * 300f);
				food = Instantiate(foodPrefab, transform.position+Vector3.up, Quaternion.identity) as GameObject;
				food.GetComponent<Rigidbody>().AddForce(new Vector3(Random.value-0.5f, 1f, Random.value-0.5f) * 300f);
			}

			if(timer >= eatTime) {
				eating = false;
				foodToDisable.SetActive(false);
			}
		}
	}

	void OnCollisionWithPlayer(GameObject player) {
		eating = true;
		alreadyEaten = true;
	}
}
