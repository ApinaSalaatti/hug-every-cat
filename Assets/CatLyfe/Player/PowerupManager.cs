using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddPowerup(PowerupEffectType type, float amount) {
		switch(type) {
		case PowerupEffectType.JUMP:
			break;
		case PowerupEffectType.SPEED:
			GetComponent<PlayerMover>().Speed += amount;
			break;
		}
	}
}
