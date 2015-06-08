using UnityEngine;
using System.Collections;

public class CatBrain : MonoBehaviour {
	private CatMover mover;
	public CatMover Mover { get { return mover; } }

	// Use this for initialization
	void Awake() {
		mover = GetComponent<CatMover>();
	}
	
	private float timer = 0f;
	private bool moving = false;

	void CatUpdate(float deltaTime) {
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
