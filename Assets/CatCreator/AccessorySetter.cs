using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AccessorySetter : MonoBehaviour {
	[SerializeField]
	private CatAccessories accessories;
	[SerializeField]
	private Image previewImage;

	private string[] hats;
	private int hatIndex;

	// Use this for initialization
	void Start () {
		string[] availableHats = CatAccessoryManager.Instance.AvailableHats.ToArray();

		hats = new string[availableHats.Length+1];
		hats[0] = null;
		for(int i = 0; i < availableHats.Length; i++) {
			Debug.Log(availableHats[i]);
			hats[i+1] = availableHats[i];
		}
		Debug.Log(hats.Length);
		hatIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeHat(int changeInIndex) {
		hatIndex += changeInIndex;
		if(hatIndex < 0) {
			hatIndex = hats.Length-1;
		}
		else if(hatIndex > hats.Length-1) {
			hatIndex = 0;
		}
		Sprite hat = CatAccessoryManager.Instance.GetHat(hats[hatIndex]);
		previewImage.sprite = hat;

		accessories.SetHat(hat);
	}
}
