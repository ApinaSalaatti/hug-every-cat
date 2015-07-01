using UnityEngine;
using System.Collections;

/*
 * This goal tries to satisfy a given need for the cat using the following steps:
 * 1. see if you know about an object that satisfies the given need, if yes go to 3.
 * 2. see if there are new objects you haven checked out yet. If yes add a CheckItemOut subgoal and return to 1, else just cry
 * 3. Plan a path to the item
 * 4. Use item!
 */
public class SatisfyNeedGoal : CompositeGoal {
	private CatNeedType need;

	private HouseItem chosenItem;

	private bool searching = false;

	public SatisfyNeedGoal(CatBrain owner, CatNeedType n) : base(owner) {
		need = n;
	}

	public override void Activate () {
		//Debug.Log("Activating SatisfyNeed");
		CurrentStatus = GoalStatus.ACTIVE;
		searching = false;

		ClearSubgoals();

		GameObject closest = Owner.Perceptions.GetItemThatSatisfiesNeed(need, true);
		if(closest != null) {
			//Debug.Log("Found an item");
			HouseItem hi = closest.GetComponent<HouseItem>();
			chosenItem = hi;
			AddSubgoal(new MoveToPositionGoal(Owner, hi.GetUsePosition().position));
			AddSubgoal(new UseItemGoal(Owner, hi));

		}
		else {
			//Debug.Log("No item found");
			GameObject possible = Owner.Perceptions.GetUncheckedItem();
			if(possible != null) {
				//Debug.Log("Possible item found");
				searching = true;
				AddSubgoal(new CheckNewItemGoal(Owner, possible.GetComponent<HouseItem>()));
			}
			else {
				// No way to satisfy the need :(
				//Debug.Log("Nothing to be done :/");
				CurrentStatus = GoalStatus.FAILED;
			}
		}
	}

	public override GoalStatus Update (float deltaTime) {
		ActivateIfInactive();

		if(chosenItem != null && !chosenItem.CanSatisfyNeed(need)) {
			// Oh snap, the item has stopped working
			CurrentStatus = GoalStatus.FAILED;
			return CurrentStatus;
		}

		if(Owner.Perceptions.NeedMonitor.IsNeedSatisfied(need)) {
			// Hey cool, we're done!
			CurrentStatus = GoalStatus.COMPLETED;
			return CurrentStatus;
		}

		GoalStatus s = UpdateSubgoals(deltaTime);

		if(searching) {
			if(s == GoalStatus.COMPLETED) {
				// Maybe we found a proper item, let's try again!
				Activate();
			}
			else if(s == GoalStatus.FAILED) {
				// Even the search failed, let's just bail out
				CurrentStatus = GoalStatus.FAILED;
			}
		}
		else {
			// If we were trying to satisfy our need, we have already done all we can
			CurrentStatus = s;
		}

		return CurrentStatus;
	}

	public override void Terminate ()
	{
		base.Terminate ();
		//Debug.Log("Terminating SatisfyNeed");
	}

	public override bool HandleMessage (AIMessage msg)
	{
		if(msg.Type == AIMessageType.NEED_LOW || msg.Type == AIMessageType.NEED_CRITICALLY_LOW) {
			CatNeedType n = (CatNeedType)msg.Data;
			if(n == need) {
				// Already handling this, nothing to worry about!
				return true;
			}
		}
		return false;
	}
}
