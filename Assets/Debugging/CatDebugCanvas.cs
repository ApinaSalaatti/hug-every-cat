using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatDebugCanvas : MonoBehaviour {
	[SerializeField]
	private GameObject cat;
	
	[SerializeField]
	private Text catGoalsText;
	[SerializeField]
	private Text uncheckedItemsText;
	[SerializeField]
	private Text hungerItemsText;
	
	private CatBrain brain;
	private CatGoalAI ai;
	
	// Use this for initialization
	void OnAddedToWorld() {
		Debug.Log("Starting debugging");
		brain = cat.GetComponentInChildren<CatBrain>();
		ai = brain.ai as CatGoalAI;
	}
	
	// Update is called once per frame
	void CatUpdate () {
		SetGoalsText();	
		SetUncheckedItemsText();
		SetHungerItemsText();
	}
	
	private void SetGoalsText() {
		catGoalsText.text = "Current Goal:\n";
		Goal current = ai.Debug_GetCurrentGoal();
		if(current != null)
			catGoalsText.text += " - " + ai.Debug_GetCurrentGoal().ToString() + "\n";
		else
			catGoalsText.text += " - NONE\n";
		catGoalsText.text += "Goal Queue:\n";
		foreach(Goal g in ai.Debug_GetGoalQueue()) {
			catGoalsText.text += " - " + g.ToString() + "\n";
		}
	}
	
	private void SetUncheckedItemsText() {
		uncheckedItemsText.text = "Unchecked items:\n";
		foreach(GameObject i in brain.Perceptions.Debug_GetUncheckedItems()) {
			uncheckedItemsText.text += " - " + i.name + "\n";
		}
	}
	
	private void SetHungerItemsText() {
		hungerItemsText.text = "Hunger items:\n";
		foreach(GameObject i in brain.Perceptions.Debug_GetNeedItems(CatNeedType.HUNGER)) {
			hungerItemsText.text += " - " + i.name + "\n";
		}
	}
}
