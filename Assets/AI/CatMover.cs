using UnityEngine;
using System.Collections;

public class CatMover : MonoBehaviour {
	// Movement should be a normalized vector
	private Vector2 movement = new Vector2(0f, 0f);
	private float speed = 2f;

	private Animator animator;

	private bool movingLeft = false;

	public void SetMovement(float x, float y) {
		movement.x = x;
		movement.y = y;
		if(movement.sqrMagnitude > 0.000001f) {
			movement = movement.normalized;
		}
		else {
			movement.x = 0f;
			movement.y = 0f;
		}
	}

	// Use this for initialization
	void Awake() {
		animator = GetComponent<Animator>();
	}

	private void FlipSprite() {
		movingLeft = !movingLeft;

		Vector3 scale = transform.localScale;
		scale.x *= -1f;
		transform.localScale = scale;
	}
	
	// Update is called once per frame
	void CatUpdate(float deltaTime) {
		if(movement.sqrMagnitude > 0.000001f) {
			if(movement.x < 0f && !movingLeft) {
				FlipSprite();
			}
			else if(movement.x > 0f && movingLeft) {
				FlipSprite();
			}

			animator.SetBool("Moving", true);
			Vector3 pos = transform.position;
			pos.x += movement.x * speed * deltaTime;
			pos.y += movement.y * speed * deltaTime;
			transform.position = pos;
		}
		else {
			animator.SetBool("Moving", false);
		}
	}
}
