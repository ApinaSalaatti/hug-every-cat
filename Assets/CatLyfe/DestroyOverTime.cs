﻿using UnityEngine;
using System.Collections;

public class DestroyOverTime : MonoBehaviour {
	[SerializeField]
	private float timeToLive;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeToLive -= Time.deltaTime;
		if(timeToLive <= 0f) {
			Destroy(gameObject);
		}
	}
}
