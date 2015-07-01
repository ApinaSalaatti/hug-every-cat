using UnityEngine;
using System.Collections;

public class CheckNewItemGoal : CompositeGoal {
	private HouseItem itemToCheck;

	public CheckNewItemGoal(CatBrain brain, HouseItem item) : base(brain) {
		itemToCheck = item;
	}

	public override void Activate ()
	{
		//Debug.Log("Activating CheckNewItem");

		CurrentStatus = GoalStatus.ACTIVE;

		if(Owner.Perceptions.ItemIsUnchecked(itemToCheck.gameObject)) {
			AddSubgoal(new MoveToPositionGoal(Owner, itemToCheck.gameObject.transform.position));
			AddSubgoal(new ExamineItemGoal(Owner, itemToCheck));
		}
		else {
			// Already checked it :O
			CurrentStatus = GoalStatus.COMPLETED;
		}
	}

	public override GoalStatus Update (float deltaTime)
	{
		ActivateIfInactive();

		GoalStatus s = UpdateSubgoals(deltaTime);

		return s;
	}

	public override void Terminate ()
	{
		base.Terminate();
		//Debug.Log("Terminating CheckNewItem");
	}
}
