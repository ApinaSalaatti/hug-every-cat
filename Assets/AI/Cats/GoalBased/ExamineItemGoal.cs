using UnityEngine;
using System.Collections;

public class ExamineItemGoal : Goal {
	private HouseItem itemToExamine;

	private float examineTime = 0f;
	private float timer = 0f;

	public ExamineItemGoal(CatBrain brain, HouseItem item) : base(brain) {
		itemToExamine = item;
		examineTime = Random.Range(1f, 3f);
	}
	
	public override void Activate ()
	{
		//Debug.Log("Activating Examine");

		CurrentStatus = GoalStatus.ACTIVE;

		if(!Owner.Perceptions.ItemIsUnchecked(itemToExamine.gameObject)) {
			// Oh we already did this
			CurrentStatus = GoalStatus.COMPLETED;
		}
	}
	
	public override GoalStatus Update (float deltaTime)
	{
		ActivateIfInactive();

		timer += deltaTime;
		if(timer >= examineTime) {
			Owner.Perceptions.CheckItem(itemToExamine.gameObject);
			return GoalStatus.COMPLETED;
		}

		return CurrentStatus;
	}
	
	public override void Terminate ()
	{
		//Debug.Log("Terminating Examine");
	}
}
