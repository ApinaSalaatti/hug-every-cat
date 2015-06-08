using UnityEngine;
using System.Collections;

// An item that can be placed in the house and used by the cats
public class HouseItem : MonoBehaviour {
	[SerializeField]
	private CatNeedType satisfiedNeed; // The need this item satisfies when used, if any
	[SerializeField]
	private int amountSatisfied;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
