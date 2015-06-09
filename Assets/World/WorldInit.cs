using UnityEngine;
using System.Collections;

public class WorldInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject c = CatFactory.Instance.CreateFromFile("startingCat");
		CatManager.Instance.AddCat(c);

		HouseItem food = FindObjectOfType<HouseItem>();
		HouseItemManager.Instance.AddItem(food.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
