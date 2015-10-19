using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomNoiseDisplay : MonoBehaviour {
	[SerializeField]
	private Image alertnessBar;
	private RectTransform alertnessBarRect;
	private float maxHeight;

	[SerializeField]
	private Room room;
	
	[SerializeField]
	private Image alertnessImage;
	[SerializeField]
	private Sprite[] alertnessSprites;

	private int currentShownLevel = 0;
	
	// Use this for initialization
	void Start () {
		alertnessBarRect = alertnessBar.GetComponent<RectTransform>();
		maxHeight = alertnessBarRect.sizeDelta.y;

		alertnessImage.sprite = alertnessSprites[0];
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 size = alertnessBarRect.sizeDelta;
		float percent = room.CurrentNoise / room.RequiredNoise;
		size.y = percent * maxHeight;
		size.y = Mathf.Min(size.y, maxHeight);
		alertnessBarRect.sizeDelta = size;

		int level = Mathf.FloorToInt(percent * 10) / 2;
		if(level > currentShownLevel && level <= 5) {
			currentShownLevel++;
			AlertnessChanged(currentShownLevel);
		}
	}
	
	private void AlertnessChanged(int level) {
		alertnessImage.sprite = alertnessSprites[level];
	}
}
