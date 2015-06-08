using UnityEngine;
using System.Collections;

public enum BodyType { SKINNY, MEDIUM, FAT }

public class CatStats : MonoBehaviour {
	public string Name {
		get; set;
	}
	public BodyType BodyType {
		get; set;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
