using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// A goal that consists of multiple subgoals
public abstract class CompositeGoal : Goal {
	private List<Goal> subgoals;

	public CompositeGoal(CatBrain owner) : base(owner) {
		subgoals = new List<Goal>();
	}

	public override void AddSubgoal (Goal g) {
		subgoals.Add(g);
	}
	public void ClearSubgoals() {
		if(subgoals.Count > 0) {
			if(subgoals[0].CurrentStatus != GoalStatus.INACTIVE)
				subgoals[0].Terminate(); // The first goal might have been activated
			subgoals.Clear();
		}
	}

	public GoalStatus UpdateSubgoals(float deltaTime) {
		// Remove completed and failed subgoals from front
		while(subgoals.Count > 0 && (subgoals[0].CurrentStatus == GoalStatus.COMPLETED || subgoals[0].CurrentStatus == GoalStatus.FAILED)) {
			Goal g = subgoals[0];
			subgoals.RemoveAt(0);
			g.Terminate();
		}

		// Now process the first goal if any are left
		if(subgoals.Count > 0) {
			GoalStatus status = subgoals[0].Update(deltaTime);
			if(status == GoalStatus.COMPLETED && subgoals.Count > 1) {
				// Front goal completed but more goals are in queue, so this goal is not yet completed
				return GoalStatus.ACTIVE;
			}
			return status;
		}
		else {
			return GoalStatus.COMPLETED;
		}
	}

	public override void Terminate ()
	{
		//Debug.Log("Terminating all subgoals");
		foreach(Goal g in subgoals) {
			g.Terminate();
		}
	}
}
