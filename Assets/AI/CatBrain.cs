using UnityEngine;
using System.Collections;

public class CatBrain : MonoBehaviour {
	private CatMover mover;
	public CatMover Mover { get { return mover; } }

	public HouseItem food;

	// Use this for initialization
	void Awake() {
		mover = GetComponent<CatMover>();

		food = FindObjectOfType<HouseItem>();
	}
	
	private float timer = 0f;
	private bool moving = false;
	private bool eating = false;

	void CatUpdate(float deltaTime) {
		if(!eating) {
			eating = true;
			food.StartUse(gameObject);
		}

		timer += deltaTime;
		if(timer >= 2f) {
			timer = 0f;
			moving = !moving;
			if(moving)
				mover.SetMovement(Random.value, Random.value);
			else
				mover.SetMovement(0f, 0f);
		}
	}
}
