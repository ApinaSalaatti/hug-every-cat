using UnityEngine;
using System.Collections;

public class NeedMonitor : MonoBehaviour {
	public static float NEED_LOW_TRESHOLD = 35f;
	public static float NEED_CRITICALLY_LOW_TRESHOLD = 10f;
	public static float NEED_SATISFIED_TRESHOLD = 90f;

	private CatNeeds needs;

	private float lastHunger;
	private float lastCleanliness;
	private float lastHappiness;
	private float lastEnergy;

	void Start() {
		needs = GetComponent<CatBrain>().Cat.GetComponent<CatNeeds>();
	}

	void CatUpdate (float deltaTime) {
		CheckNeeds();
	}

	private void CheckNeeds() {
		if(needs.Hunger < NEED_LOW_TRESHOLD && lastHunger >= NEED_LOW_TRESHOLD) {
			gameObject.SendMessage("AIMessage", new AIMessage(AIMessageType.NEED_LOW, CatNeedType.HUNGER));
		}
		if(needs.Cleanliness < NEED_LOW_TRESHOLD && lastCleanliness >= NEED_LOW_TRESHOLD) {
			gameObject.SendMessage("AIMessage", new AIMessage(AIMessageType.NEED_LOW, CatNeedType.CLEANLINESS));
		}
		if(needs.Happiness < NEED_LOW_TRESHOLD && lastHappiness >= NEED_LOW_TRESHOLD) {
			gameObject.SendMessage("AIMessage", new AIMessage(AIMessageType.NEED_LOW, CatNeedType.HAPPINESS));
		}
		if(needs.Energy < NEED_LOW_TRESHOLD && lastEnergy >= NEED_LOW_TRESHOLD) {
			gameObject.SendMessage("AIMessage", new AIMessage(AIMessageType.NEED_LOW, CatNeedType.ENERGY));
		}
		lastHunger = needs.Hunger;
		lastCleanliness = needs.Cleanliness;
		lastHappiness = needs.Happiness;
		lastEnergy = needs.Energy;
	}

	public float GetNeed(CatNeedType need) {
		switch(need) {
		case CatNeedType.HUNGER:
			return needs.Hunger;
		case CatNeedType.CLEANLINESS:
			return needs.Cleanliness;
		case CatNeedType.HAPPINESS:
			return needs.Happiness;
		case CatNeedType.ENERGY:
			return needs.Energy;
		default:
			return 0f;
		}
	}

	public bool IsNeedLow(CatNeedType need) {
		switch(need) {
		case CatNeedType.HUNGER:
			return needs.Hunger < NEED_LOW_TRESHOLD;
		case CatNeedType.CLEANLINESS:
			return needs.Cleanliness < NEED_LOW_TRESHOLD;
		case CatNeedType.HAPPINESS:
			return needs.Happiness < NEED_LOW_TRESHOLD;
		case CatNeedType.ENERGY:
			return needs.Energy < NEED_LOW_TRESHOLD;
		default:
			return false;
		}
	}

	public bool IsNeedSatisfied(CatNeedType need) {
		switch(need) {
		case CatNeedType.HUNGER:
			return needs.Hunger >= NEED_SATISFIED_TRESHOLD;
		case CatNeedType.CLEANLINESS:
			return needs.Cleanliness >= NEED_SATISFIED_TRESHOLD;
		case CatNeedType.HAPPINESS:
			return needs.Happiness >= NEED_SATISFIED_TRESHOLD;
		case CatNeedType.ENERGY:
			return needs.Energy >= NEED_SATISFIED_TRESHOLD;
		default:
			return true;
		}
	}
}
