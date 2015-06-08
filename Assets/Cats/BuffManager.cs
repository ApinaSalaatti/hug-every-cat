using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuffManager : MonoBehaviour {
	private List<Buff> buffs = new List<Buff>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void CatUpdate (float deltaTime) {
	
	}

	public void AddBuff(Buff b) {
		buffs.Add(b);
	}
}
