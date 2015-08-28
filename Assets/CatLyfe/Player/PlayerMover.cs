using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {
	private CharacterController controller;
	private Animator animator;
	
	private float speed = 8f;
	public float Speed {
		get { return speed; }
		set { speed = value; }
	}

	private float jumpForce = 10f;
	private float gravityForce = 30f;

	private float currentYSpeed = 0f;

	[SerializeField]
	private LayerMask groundCheckLayerMask;

	private bool groundedLastFrame = false;
	private float jumpAllowedTimer = 0f;

	// Use this for initialization
	void Awake() {
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		currentYSpeed -= gravityForce * Time.deltaTime;

		bool tryingToJump = Input.GetButtonDown("Jump");
		bool grounded = false;
		Ray ray = new Ray(transform.position, Vector3.down);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 0.52f, groundCheckLayerMask.value)) {
			grounded = true;
		}

		if(grounded) {
			currentYSpeed = 0f;
		}
		else {
			jumpAllowedTimer -= Time.deltaTime;
			if(groundedLastFrame) {
				jumpAllowedTimer = 0.3f;
			}
		}

		if(tryingToJump && (grounded || jumpAllowedTimer > 0f)) {
			currentYSpeed = jumpForce;
		}
		
		groundedLastFrame = grounded && !tryingToJump;

		Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		//Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 1f);

		movement = movement.normalized * speed;

		animator.SetBool("Moving", movement.sqrMagnitude > 0f);

		// If moving, make the cat face the way it's going
		if(movement.sqrMagnitude > 0f)
			gameObject.transform.forward = movement;

		movement.y = currentYSpeed;
		controller.Move(movement * Time.deltaTime);
	}
}
