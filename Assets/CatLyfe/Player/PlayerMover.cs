using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour, InputReceiver {
	private CharacterController controller;
	private Animator animator;
	
	private float speed = 12f;
	public float Speed {
		get { return speed; }
		set { speed = value; }
	}

	[SerializeField]
	private Transform[] groundCheckPositions;

	private float jumpForce = 10f;
	private float gravityForce = 30f;

	private float currentYSpeed = 0f;

	[SerializeField]
	private LayerMask groundCheckLayerMask;

	private bool groundedLastFrame = false;
	private float jumpAllowedTimer = 0f;

	private bool firstPerson = false;
	void ToggleFirstPerson(bool fp) {
		firstPerson = fp;
	}

	// Because the player mover can be enabled/disabled from many sources, this keeps track of the amount of disables
	// This is to ensure the mover only gets re-enabled once nobody wants it to be disabled
	private int disableTimes = 0;
	public void Enable(bool enable) {
		if(enable) {
			disableTimes--;
			if(disableTimes <= 0) {
				disableTimes = 0;
				this.enabled = true;
			}
		}
		else {
			disableTimes++;
			this.enabled = false;
		}
	}

	// Use this for initialization
	void Awake() {
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
	}

	public bool ReceiveInput() {
		if(!enabled)
			return false;

		currentYSpeed -= gravityForce * Time.deltaTime;

		bool tryingToJump = Input.GetButtonDown("Jump");
		bool grounded = false;
		for(int i = 0; i < groundCheckPositions.Length; i++) {
			Ray ray = new Ray(groundCheckPositions[i].position, Vector3.down);
			RaycastHit hit;
			//Debug.DrawRay(groundCheckPositions[i].position, Vector3.down, Color.red, 0.541f);
			if(Physics.Raycast(ray, out hit, 0.541f, groundCheckLayerMask.value)) {
				grounded = true;
			}
		}

		if(grounded && currentYSpeed < 0f) {
			currentYSpeed = 0f;
		}
		else {
			jumpAllowedTimer -= Time.deltaTime;
			if(groundedLastFrame) {
				jumpAllowedTimer = 0.5f;
			}
		}

		//Debug.Log(controller.isGrounded + " & " + grounded);
		if(tryingToJump && (grounded || controller.isGrounded || jumpAllowedTimer > 0f)) {
			currentYSpeed = jumpForce;
		}
		//Debug.Log(currentYSpeed);
		
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

		return true;
	}

	private void NormalMovement() {

	}

	private void FirstPersonMovement() {

	}
}
