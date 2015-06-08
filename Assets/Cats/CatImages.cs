using UnityEngine;
using System.Collections;

[System.Serializable]
public class CatBodyData {
	public Sprite emptyCat;
	public Sprite legalPixels;
	public Animation idleAnim;
	public Animation walkAnim;
}

public class CatImages : MonoBehaviour {
	[SerializeField]
	private CatBodyData skinnyCat;
	public CatBodyData Skinny { get { return skinnyCat; } }

	[SerializeField]
	private CatBodyData mediumCat;
	public CatBodyData Medium { get { return mediumCat; } }

	[SerializeField]
	private CatBodyData fatCat;
	public CatBodyData Fat { get { return fatCat; } }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
