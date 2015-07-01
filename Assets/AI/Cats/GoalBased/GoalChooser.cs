using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalChooser {
	private class GoalEvaluatorQueueItem {
		public float weight;
		public GoalEvaluator evaluator;
		public GoalEvaluatorQueueItem(float w, GoalEvaluator e) {
			weight = w;
			evaluator = e;
		}
	}

	private CatGoalAI ai;

	private List<GoalEvaluator> evaluators;

	public GoalChooser(CatGoalAI a) {
		ai = a;

		evaluators = new List<GoalEvaluator>();
		evaluators.Add(new SatisfyNeedEvaluator(a, CatNeedType.HUNGER));
		evaluators.Add(new SatisfyNeedEvaluator(a, CatNeedType.CLEANLINESS));
		evaluators.Add(new SatisfyNeedEvaluator(a, CatNeedType.HAPPINESS));
		evaluators.Add(new SatisfyNeedEvaluator(a, CatNeedType.ENERGY));
		evaluators.Add(new CheckNewItemEvaluator(a));
	}
	
	public void Think() {
		//Debug.Log("Starting goal choosing");

		List<GoalEvaluatorQueueItem> q = new List<GoalEvaluatorQueueItem>();

		foreach(GoalEvaluator ev in evaluators) {
			float w = ev.Evaluate();
			if(w > 0f)
				q.Add(new GoalEvaluatorQueueItem(w, ev));
		}

		// Put the goals in order so the one with the highest weight will be added first
		q.Sort(delegate(GoalEvaluatorQueueItem x, GoalEvaluatorQueueItem y) {
			return x.weight.CompareTo(y.weight);
		});

		foreach(GoalEvaluatorQueueItem ge in q) {
			ai.AddGoal(ge.evaluator.GiveGoal());
		}

		if(ai.GoalsInQueue() == 0) {
			// It seems we could not figure out anything to do. Let's just wander around
			//Debug.Log("Nothing to do, let's wander around");
			ai.AddGoal(new WanderGoal(ai.Brain));
		}

		//Debug.Log("Goal choosing ends");
	}

	public void HandleFailure(Goal g) {
		if(g is SatisfyNeedGoal) {
			// Oh man, satisfying a need went sour! Let's add it last on the queue and hope it goes better
			ai.AddGoal(g);
		}
	}
}
