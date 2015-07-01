using UnityEngine;
using System.Collections;

public class WanderGoal : Goal {

	public WanderGoal(CatBrain brain) : base(brain) {

	}

	public override void Activate ()
	{
		//Debug.Log("Activating Wander");
		CurrentStatus = GoalStatus.ACTIVE;
		Owner.Steering.WanderOn();
	}

	public override GoalStatus Update (float deltaTime)
	{
		ActivateIfInactive();

		return CurrentStatus;
	}

	public override void Terminate ()
	{
		//Debug.Log("Terminating Wander");
		Owner.Steering.WanderOff();
	}

	public override bool HandleMessage (AIMessage msg)
	{
		// Currently we just stop wandering on any message
		CurrentStatus = GoalStatus.COMPLETED;
		return true;
	}
}
