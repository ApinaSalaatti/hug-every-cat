using UnityEngine;
using System.Collections;

public enum CatNeedType { HUNGER, CLEANLINESS, HAPPINESS, ENERGY, NO_NEED }

public class CatNeeds : MonoBehaviour {
	public int Hunger {
		get; set;
	}
	public int Cleanliness {
		get; set;
	}
	public int Happiness {
		get; set;
	}
	public int Energy {
		get; set;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void CatUpdate () {
	
	}
}
