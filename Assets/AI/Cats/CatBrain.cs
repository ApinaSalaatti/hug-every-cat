using UnityEngine;
using System.Collections;

public class CatBrain : MonoBehaviour {
	[SerializeField]
	private GameObject cat;
	public GameObject Cat { get { return cat; } }

	//private CatMover mover;
	//public CatMover Mover { get { return mover; } }

	private CatSteering steering;
	public CatSteering Steering { get { return steering; } }

	private CatPerceptions perceptions;
	public CatPerceptions Perceptions { get { return perceptions; } }

	public BaseCatAI ai;

	// Use this for initialization
	void Awake() {
		// References from the main Cat object
		//mover = cat.GetComponent<CatMover>();

		// Recerences from the Brain object
		steering = GetComponent<CatSteering>();
		perceptions = GetComponent<CatPerceptions>();

		ai = new CatGoalAI(this);
	}

	void CatUpdate(float deltaTime) {
		ai.AIUpdate(deltaTime);
		steering.Calculate();
	}

	void AIMessage(AIMessage msg) {
		ai.HandleMessage(msg);
	}
}
