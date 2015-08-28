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

	[SerializeField]
	private Image[] alertnessLevelIndicators;
	private int indicatorsShown = 0;

	// Use this for initialization
	void Start () {
		alertnessBarRect = alertnessBar.GetComponent<RectTransform>();
		maxHeight = alertnessBarRect.sizeDelta.y;

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
			alertnessImage.sprite = alertnessSprites[indicatorsShown];
			alertnessLevelIndicators[indicatorsShown].gameObject.SetActive(false);
			descriptionText.text = alertnessDescriptions[indicatorsShown];
		}
	}

	private void AlertnessChanged(int alertness) {
		alertnessImage.sprite = alertnessSprites[alertness];
		alertnessLevelIndicators[alertness-1].gameObject.SetActive(true);
		descriptionText.text = alertnessDescriptions[alertness];
	}
}
