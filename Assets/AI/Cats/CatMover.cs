using UnityEngine;
using System.Collections;

public class CatMover : MonoBehaviour {
	private Vector2 movement = new Vector2(0f, 0f); // Movement as units per second, currently
	public Vector2 Movement { get { return movement; } }

	private float speed = 2f;
	public float Speed {
		get { return speed; }
	}

	private Animator animator;
	private CatSpriteManager spriteManager;

	public void SetMovement(float x, float y) {
		movement.x = x;
		movement.y = y;
		if(movement.sqrMagnitude > 0.0001f) {
			// Cool we are actually moving
		}
		else {
			// Probably a mistake, just zero everything out
			movement.x = 0f;
			movement.y = 0f;
		}
	}

	// Use this for initialization
	void Awake() {
		animator = GetComponent<Animator>();
		spriteManager = GetComponent<CatSpriteManager>();
	}

	// Update is called once per frame
	void CatUpdate(float deltaTime) {
		if(movement.sqrMagnitude > 0.0001f) {
			if(movement.x < 0f) {
				spriteManager.LookLeft();
			}
			else if(movement.x > 0f) {
				spriteManager.LookRight();
			}

			animator.SetBool("Moving", true);
			Vector3 pos = transform.position;
			pos.x += movement.x * deltaTime;
			pos.y += movement.y * deltaTime;
			transform.position = pos;
		}
		else {
			animator.SetBool("Moving", false);
		}
	}
}
