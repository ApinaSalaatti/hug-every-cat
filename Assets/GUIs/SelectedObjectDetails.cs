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

	private GameObject shownObject;

	/*
	 * Cat detail stuff
	 */
	//private GameObject shownCat;
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

	public void CenterCameraOnSelected() {
		if(shownObject != null)
			Camera.main.GetComponent<CameraMover>().CenterOnObject(shownObject);
	}

	public void OpenCatInfoScreen() {
		if(shownDetailType == DetailType.CAT && shownObject != null)
			CatInfoScreen.Instance.Show(shownObject);
	}

	public void ShowDetails(GameObject g, DetailType t) {
		if(g == null) {
			HideDetails();
			return;
		}

		GetComponent<Animator>().SetBool("Open", true);

		shown = true;

		image.color = Color.white;

		shownObject = g;

		switch(t) {
		case DetailType.CAT:
			moreInfoButton.onClick.AddListener(OpenCatInfoScreen);
			catStats = g.GetComponent<CatStats>();
			catNeeds = g.GetComponent<CatNeeds>();
			nameText.text = catStats.Name;
			image.sprite = g.GetComponent<CatSpriteManager>().GetSprite();
			image.color = Color.white;
			catNeedsIndicatorObject.SetActive(true);
			break;
		case DetailType.HOUSE_ITEM:
			nameText.text = g.GetComponent<HouseItem>().ItemName;
			SpriteRenderer sr = g.GetComponent<SpriteRenderer>();
			image.sprite = sr.sprite;
			image.color = sr.color; // Item might be tinted
			break;
		}

		shownDetailType = t;
	}
	public void HideDetails() {
		shown = false;
		shownObject = null;

		GetComponent<Animator>().SetBool("Open", false);

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
