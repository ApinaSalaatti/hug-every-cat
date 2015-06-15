using UnityEngine;
using System.Collections;

public enum CatNeedType { HUNGER, CLEANLINESS, HAPPINESS, ENERGY, NO_NEED }

public class CatNeeds : MonoBehaviour {
	// The maximum satisfaction for any need
	public static float NeedMaximum { get { return 100f; } }

	public float Hunger {
		get; set;
	}
	public float Cleanliness {
		get; set;
	}
	public float Happiness {
		get; set;
	}
	public float Energy {
		get; set;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void CatUpdate (float deltaTime) {
	
	}

	public void IncreaseNeed(CatNeedType type, float amount) {
		switch(type) {
		case CatNeedType.HUNGER:
			Hunger += amount;
			break;
		case CatNeedType.CLEANLINESS:
			Cleanliness += amount;
			break;
		case CatNeedType.HAPPINESS:
			Happiness += amount;
			break;
		case CatNeedType.ENERGY:
			Energy += amount;
			break;
		}
	}
	public void NeedIncreased(NeedIncreasedEffect effect) {
		IncreaseNeed(effect.type, effect.amount);
	}
}
