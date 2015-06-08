using UnityEngine;
using System.Collections;

public class WorldInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject c = Globals.CatFactory.CreateFromFile("startingCat");
		CatManager.AddCat(c);
		//CatInfoScreen.Instance().Show(c);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
