using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HumanAlertnessMeter : MonoBehaviour {
	[SerializeField]
	private Image alertnessBar;
	private RectTransform alertnessBarRect;
	private float maxHeight;

	[SerializeField]
	private Image alertnessImage;
	[SerializeField]
	private Sprite[] alertnessSprites;
	[SerializeField]
	private string[] alertnessDescriptions;
	[SerializeField]
	private Text descriptionText;

	private Color[] alertnessColors;

	[SerializeField]
	private Image[] alertnessLevelIndicators;
	private int indicatorsShown = 0;

	// Use this for initialization
	void Start () {
		alertnessBarRect = alertnessBar.GetComponent<RectTransform>();
		maxHeight = alertnessBarRect.sizeDelta.y;

		alertnessColors = new Color[alertnessLevelIndicators.Length];
		for(int i = 0; i < alertnessColors.Length; i++) {
			alertnessColors[i] = alertnessLevelIndicators[i].color;
			alertnessLevelIndicators[i].color = Color.gray;
		}

		alertnessImage.sprite = alertnessSprites[0];
		descriptionText.text = alertnessDescriptions[0];
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 size = alertnessBarRect.sizeDelta;
		size.y = (HumanAlertness.Instance.CurrentAlertness / HumanAlertness.Instance.MaxAlertness) * maxHeight;
		alertnessBarRect.sizeDelta = size;

		if(HumanAlertness.Instance.AlertnessLevel > indicatorsShown) {
			indicatorsShown++;
			AlertnessChanged(indicatorsShown);
		}
		else if(HumanAlertness.Instance.AlertnessLevel < indicatorsShown) {
			// This SHOULD mean that a chase has ended
			indicatorsShown--;
			AlertnessChanged(indicatorsShown);
			//alertnessImage.sprite = alertnessSprites[indicatorsShown];
			//alertnessLevelIndicators[indicatorsShown].gameObject.SetActive(false);
			//descriptionText.text = alertnessDescriptions[indicatorsShown];
		}
	}

	private void AlertnessChanged(int alertness) {
		alertnessImage.sprite = alertnessSprites[alertness];
		//alertnessLevelIndicators[alertness-1].gameObject.SetActive(true);

		// Reset all DEFCON colors
		for(int i = 0; i < alertnessLevelIndicators.Length; i++) {
			alertnessLevelIndicators[i].color = Color.gray;
		}
		// Set color of correct DEFCON level
		alertnessLevelIndicators[alertness-1].color = alertnessColors[alertness-1];
		descriptionText.text = alertnessDescriptions[alertness];
	}
}
