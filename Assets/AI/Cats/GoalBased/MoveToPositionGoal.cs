using UnityEngine;
using System.Collections;

public class MoveToPositionGoal : Goal {
	private Vector3 targetPos;

	public MoveToPositionGoal(CatBrain brain, Vector3 pos) : base(brain) {
		targetPos = pos;
	}

	public override void Activate ()
	{
		CurrentStatus = GoalStatus.ACTIVE;

		//Debug.Log("Activating MoveToPosition");
		//Vector3 toTarget = targetPos - Owner.Cat.transform.position;
		//Owner.Mover.SetMovement(toTarget.x, toTarget.y);
		Owner.Steering.ArriveOn(targetPos);
	}

	public override GoalStatus Update (float deltaTime)
	{
		ActivateIfInactive();

		//Debug.Log("Updating MoveToPosition");

		float dist = Vector3.Distance(targetPos, Owner.Cat.transform.position);
		if(dist <= 0.01f) {
			CurrentStatus = GoalStatus.COMPLETED;
		}
		else {
			CurrentStatus = GoalStatus.ACTIVE;
		}
		return CurrentStatus;
	}

	public override void Terminate ()
	{
		//Debug.Log("Terminating MoveToPosition");
		Owner.Steering.ArriveOff();
	}

	public override string ToString ()
	{
		return "[MoveToPositionGoal]: " + targetPos;
	}
}
