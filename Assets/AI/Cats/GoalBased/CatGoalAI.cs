using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatGoalAI : BaseCatAI {
	private List<Goal> goalQueue;
	private Goal currentGoal;

	public void AddGoal(Goal g) {
		goalQueue.Add(g);
	}
	public void RemoveGoal(Goal g) {
		goalQueue.Remove(g);
	}
	public int GoalsInQueue() {
		return goalQueue.Count;
	}

	private GoalChooser chooser;

	// Use this for initialization
	public CatGoalAI(CatBrain brain) : base(brain) {
		//Debug.Log("Creating CatGoalAI");
		goalQueue = new List<Goal>();

		currentGoal = new WanderGoal(Brain);

		chooser = new GoalChooser(this);
	}
	
	public override void AIUpdate(float deltaTime) {
		//Debug.Log("Updating CatGoalAI");

		if(currentGoal != null) {
			GoalStatus s =  currentGoal.Update(deltaTime);

			if(s == GoalStatus.FAILED) {
				currentGoal.Terminate();
				chooser.HandleFailure(currentGoal);
				currentGoal = null;
				SetNewGoal();
			}
			else if(s == GoalStatus.COMPLETED) {
				currentGoal.Terminate();
				currentGoal = null;
				SetNewGoal();
			}
		}
		else {
			// Now current goal, figure something out
			SetNewGoal();
		}
	}

	private void HandleFailure(Goal g) {
		if(g is SatisfyNeedGoal) {
			// Oh man, satisfying a need went sour! Let's add it last on the queue and hope it goes better
			goalQueue.Add(g);
		}
	}

	private void SetNewGoal() {
		if(goalQueue.Count > 0) {
			currentGoal = goalQueue[0];
			goalQueue.RemoveAt(0);
		}
		else {
			chooser.Think();
		}
	}

	public override bool HandleMessage (AIMessage msg) {
		if(currentGoal != null && currentGoal.HandleMessage(msg)) {
			return true;
		}
		else {
			return false;
		}
	}

	/* 
	 * ================================
	 * =    FOR DEBUGGING PURPOSES    =
	 * ================================
	 */
	public Goal Debug_GetCurrentGoal() {
		return currentGoal;
	}
	public List<Goal> Debug_GetGoalQueue() {
		return goalQueue;
	}
}
