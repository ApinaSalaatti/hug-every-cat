using UnityEngine;
using System.Collections;

public abstract class GoalEvaluator {
	protected CatGoalAI ai;

	public GoalEvaluator(CatGoalAI ai) {
		this.ai = ai;
	}

	public abstract float Evaluate();
	public abstract Goal GiveGoal();
}

public class SatisfyNeedEvaluator : GoalEvaluator {
	private CatNeedType need;

	public SatisfyNeedEvaluator(CatGoalAI ai, CatNeedType need) : base(ai) {
		this.need = need;
	}

	public override float Evaluate ()
	{
		if(ai.Brain.Perceptions.NeedMonitor.IsNeedLow(need) && ai.Brain.Perceptions.CanSatisfyNeed(need)) {
			return NeedMonitor.NEED_LOW_TRESHOLD - ai.Brain.Perceptions.NeedMonitor.GetNeed(need);
		}
		else {
			return 0;
		}
	}
	public override Goal GiveGoal ()
	{
		return new SatisfyNeedGoal(ai.Brain, need);
	}
}

public class CheckNewItemEvaluator : GoalEvaluator {
	private HouseItem foundItem;

	public CheckNewItemEvaluator(CatGoalAI ai) : base(ai) {

	}

	public override float Evaluate ()
	{
		if(ai.Brain.Perceptions.UncheckedItemsLeft()) {
			foundItem = ai.Brain.Perceptions.GetUncheckedItem(true).GetComponent<HouseItem>();
			// TODO: make curiosity affect this!
			return 1;
		}
		else {
			return 0f;
		}
	}
	public override Goal GiveGoal ()
	{
		if(foundItem == null)
			throw new System.Exception("Trying to add CheckNewItemGoal with no new items!");

		return new CheckNewItemGoal(ai.Brain, foundItem);
	}
}
