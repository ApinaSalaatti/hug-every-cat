﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CollidableObject))]
public class BreakingObject : MonoBehaviour {
	[SerializeField]
	private string animationTriggerToSet = "Breaking";
	[SerializeField]
	private bool destroyOnBreak = false;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionWithPlayer(GameObject player) {
		gameObject.layer = LayerMask.NameToLayer("Broken");

		CollidableObject col = GetComponent<CollidableObject>();
		player.SendMessage("AddScore", new ScoreEvent(col.ScoreValue, ScoreType.BREAK, col.ScoreText, gameObject.transform.position));

		// Start the break sequence
		if(animator != null) {
			animator.SetTrigger(animationTriggerToSet);
		}
		else {
			BreakAnimationDone();
		}
	}

	public void BreakAnimationDone() {
		gameObject.SendMessage("OnBreak", SendMessageOptions.DontRequireReceiver);
		if(destroyOnBreak)
			Destroy(gameObject);
	}
}
