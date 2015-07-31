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
		hats = CatAccessoryManager.Instance.AvailableHats.ToArray();

		hatIndex = 0;
		ChangeHat(0);
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

		Debug.Log(changeInIndex);
		Debug.Log(hats[hatIndex]);
		accessories.SetHat(hat);
	}
}
