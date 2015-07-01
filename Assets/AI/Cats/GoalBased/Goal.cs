using UnityEngine;
using System.Collections;

public enum GoalStatus { INACTIVE, ACTIVE, ON_HOLD, COMPLETED, FAILED }

// An abstract class that all concrete atomic goals (i.e. goals that include only one task) can inherit
public abstract class Goal {
	private GoalStatus currentStatus;
	public GoalStatus CurrentStatus {
		get { return currentStatus; }
		set { currentStatus = value; }
	}

	private CatBrain owner;
	public CatBrain Owner { get { return owner; } }

	public Goal(CatBrain own) {
		currentStatus = GoalStatus.INACTIVE;
		owner = own;
	}

	public void ActivateIfInactive() {
		if(currentStatus == GoalStatus.INACTIVE) {
			Activate();
		}
	}

	public abstract void Activate();
	public abstract GoalStatus Update(float deltaTime);
	public abstract void Terminate();

	// Override this for goals that can receive messages
	public virtual bool HandleMessage(AIMessage msg) {
		return false;
	}

	public virtual void AddSubgoal(Goal g) {
		throw new System.Exception("Trying to add a subgoal to an atomic goal");
	}
}
