using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum DetailType { CAT, HOUSE_ITEM }

public class SelectedObjectDetails : MonoBehaviour {
	private static SelectedObjectDetails instance;
	public static SelectedObjectDetails Instance { get { return instance; } }

	/*
	 * Instance begins here
	 */
	private bool shown = false;

	[SerializeField]
	private Text nameText;
	[SerializeField]
	private Image image;

	[SerializeField]
	private Button moreInfoButton;

	private DetailType shownDetailType;

	/*
	 * Cat detail stuff
	 */
	private GameObject shownCat;
	private CatStats catStats;
	private CatNeeds catNeeds;

	[SerializeField]
	private GameObject catNeedsIndicatorObject;

	[SerializeField]
	private Image hungerIndicator;
	[SerializeField]
	private Image cleanlinessIndicator;
	[SerializeField]
	private Image happinessIndicator;
	[SerializeField]
	private Image energyIndicator;

	// Use this for initialization
	void Awake() {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(!shown) {
			return;
		}

		switch(shownDetailType) {
		case DetailType.CAT:
			UpdateCatDetails();
			break;
		case DetailType.HOUSE_ITEM:
			UpdateHouseItemDetails();
			break;
		}
	}

	private void UpdateCatDetails() {
		SetCatNeedMeter(catNeeds.Hunger, hungerIndicator);
		SetCatNeedMeter(catNeeds.Cleanliness, cleanlinessIndicator);
		SetCatNeedMeter(catNeeds.Happiness, happinessIndicator);
		SetCatNeedMeter(catNeeds.Energy, energyIndicator);
	}
	private void UpdateHouseItemDetails() {

	}

	private void SetCatNeedMeter(float needAmount, Image needIndicator) {
		float value = needAmount / CatNeeds.NeedMaximum;
		Vector2 size = needIndicator.GetComponent<RectTransform>().sizeDelta;
		size.x = value * 100f;
		needIndicator.GetComponent<RectTransform>().sizeDelta = size;
	}

	public void OpenCatInfoScreen() {
		if(shownCat != null)
			CatInfoScreen.Instance.Show(shownCat);
	}

	public void ShowDetails(GameObject g, DetailType t) {
		if(g == null) {
			HideDetails();
			return;
		}

		shown = true;

		image.color = Color.white;

		switch(t) {
		case DetailType.CAT:
			moreInfoButton.onClick.AddListener(OpenCatInfoScreen);
			shownCat = g;
			catStats = g.GetComponent<CatStats>();
			catNeeds = g.GetComponent<CatNeeds>();
			nameText.text = catStats.Name;
			image.sprite = g.GetComponent<CatSpriteManager>().GetSprite();
			catNeedsIndicatorObject.SetActive(true);
			break;
		case DetailType.HOUSE_ITEM:
			nameText.text = g.GetComponent<HouseItem>().ItemName;
			image.sprite = g.GetComponent<SpriteRenderer>().sprite;
			break;
		}

		shownDetailType = t;
	}
	public void HideDetails() {
		shown = false;

		moreInfoButton.onClick.RemoveAllListeners();

		switch(shownDetailType) {
		case DetailType.CAT:
			catNeedsIndicatorObject.SetActive(false);
			break;
		case DetailType.HOUSE_ITEM:
			break;
		}

		nameText.text = "";
		image.color = new Color(1f, 1f, 1f, 0f);
		image.sprite = null;
	}
}
