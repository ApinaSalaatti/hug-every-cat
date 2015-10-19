using UnityEngine;
using System.Collections;

public enum PowerupEffectType { SPEED, JUMP }

[RequireComponent(typeof(CollidableObject))]
public class Powerup : MonoBehaviour {
	[SerializeField]
	private GameObject pickupParticles;
	[SerializeField]
	private GameObject glowObject;
	[SerializeField]
	private GameObject jumpParticles;
	[SerializeField]
	private GameObject speedParticles;
	[SerializeField]
	private PowerupEffectType type;
	[SerializeField]
	private float amount;

	void Start() {
		switch(type) {
		case PowerupEffectType.SPEED:
			glowObject.GetComponent<Renderer>().material.color = Color.blue;
			speedParticles.SetActive(true);
			break;
		case PowerupEffectType.JUMP:
			glowObject.GetComponent<Renderer>().material.color = Color.green;
			jumpParticles.SetActive(true);
			break;
		}
	}

	void OnCollisionWithPlayer(GameObject player) {
		PowerupManager pm = player.GetComponent<PowerupManager>();
		pm.AddPowerup(type, amount);

		Instantiate(pickupParticles, transform.position, Quaternion.identity);

		Destroy(gameObject);
	}
}
