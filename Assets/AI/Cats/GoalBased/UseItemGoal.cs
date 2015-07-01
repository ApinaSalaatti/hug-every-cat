using UnityEngine;
using System.Collections;

public class UseItemGoal : Goal {
	private HouseItem itemToUse;

	public UseItemGoal(CatBrain brain, HouseItem item) : base(brain) {
		itemToUse = item;
	}

	public override void Activate ()
	{
		//Debug.Log("Activating UseItem");

		Vector3 toItem = itemToUse.transform.position - Owner.Cat.transform.position;
		if(toItem.x > 0f)
			Owner.Cat.GetComponent<CatSpriteManager>().LookRight();
		else
			Owner.Cat.GetComponent<CatSpriteManager>().LookLeft();

		if(itemToUse.StartUse(Owner.Cat)) {
			CurrentStatus = GoalStatus.ACTIVE;
		}
		else {
			CurrentStatus = GoalStatus.FAILED;
		}
	}

	public override GoalStatus Update (float deltaTime)
	{
		ActivateIfInactive();

		return CurrentStatus;
	}

	public override void Terminate ()
	{
		//Debug.Log("Terminating UseItem");
		itemToUse.StopUse(Owner.Cat);
	}
}
