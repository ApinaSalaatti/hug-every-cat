using UnityEngine;
using System.Collections;

public class CatSteering : MonoBehaviour {
	[SerializeField]
	private GameObject catToSteer;
	private CatMover mover;

	private bool usingArrive = false;
	private Vector2 arriveTarget = new Vector2();

	private bool usingWander = false;
	private Vector2 wanderVector = new Vector2();
	private float wanderTimer = 0f;

	void Awake() {
		mover = catToSteer.GetComponent<CatMover>();
	}
	void Update() {
		if(usingWander)
			wanderTimer -= Time.deltaTime;
	}

	public void ArriveOn(Vector3 target) {
		mover = catToSteer.GetComponent<CatMover>();
		usingArrive = true;
		arriveTarget.x = target.x;
		arriveTarget.y = target.y;
	}
	public void ArriveOff() {
		usingArrive = false;
	}

	public void WanderOn() {
		usingWander = true;
		ResetWander();
	}
	private void ResetWander() {
		wanderVector.x = Random.value -0.5f;
		wanderVector.y = Random.value -0.5f;
		wanderVector = wanderVector.normalized;
		wanderTimer = Random.Range(1f, 3f);
	}
	public void WanderOff() {
		usingWander = false;
	}

	public void Calculate() {
		Vector2 mov = new Vector2(0f, 0f);

		if(usingArrive) {
			Vector2 currentPos = catToSteer.transform.position;
			Vector2 toTarget = (arriveTarget - currentPos).normalized * mover.Speed;
			float dist = Vector2.Distance(arriveTarget, currentPos);
			if(dist < 1f) {
				toTarget *= dist * 2f; // 2f multiplier to make the arrival a bit faster
			}
			mov += toTarget;
		}

		if(usingWander) {
			if(wanderTimer <= 0f) {
				ResetWander();
			}
			mov += wanderVector;
		}

		mover.SetMovement(mov.x, mov.y);
	}
}
