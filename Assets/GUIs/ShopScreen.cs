using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopScreen : MonoBehaviour {
	private static ShopScreen instance;
	public static ShopScreen Instance { get { return instance; } }

	/*
	 * Instance stuff from here
	 */
	[SerializeField]
	private GameObject buttonPrefab;

	[SerializeField]
	private Text categoryText;
	[SerializeField]
	private Transform shopButtonParent;
	[SerializeField]
	private Text selectedItemDetailsText;

	private List<GameObject> shopButtons;

	private ShopItem currentlySelectedItem; 
	private GameObject itemBeingPlaced;

	private bool screenShown = false;

	// Use this for initialization
	void Awake() {
		instance = this;

		shopButtons = new List<GameObject>();
	}

	public void SetShownItems(ShopItemCategory category) {
		Clear();

		categoryText.text = category.ToString();

		List<ShopItem> items = Shop.Instance.GetItems(category);
		int amount = items.Count;
		for(int i = 0; i < amount; i++) {
			ShopItem si = items[i];
			GameObject button = Instantiate(buttonPrefab);
			button.GetComponent<Button>().onClick.AddListener(() => SetSelectedItem(si));
			button.transform.SetParent(shopButtonParent);
			Image[] imgs = button.GetComponentsInChildren<Image>();
			foreach(Image img in imgs) {
				// Stupid way to find the child object's component, because the GetComponentInChildren-method actually returns the parent
				if(img.gameObject != button) {
					img.sprite = Resources.Load<Sprite>(si.imageResource);
				}
			}
			shopButtons.Add(button);
		}
	}
	// This is mostly used by the editor because it seems you can't add functions with an enum as a parameter to button callbacks
	public void SetShownItems(int category) {
		SetShownItems(Shop.Instance.IntegerToCategory(category));
	}

	public void StartItemPlacement() {
		if(currentlySelectedItem != null && Player.Instance.Wallet.Money >= currentlySelectedItem.price) {
			ItemPlacementMode.Instance.confirmListeners += ItemPlaced;
			ItemPlacementMode.Instance.cancelListeners += CancelItemPlacement;
			itemBeingPlaced = HouseItemFactory.Instance.CreateItemFromResource(currentlySelectedItem.resourceName);
			ItemPlacementMode.Instance.Begin(itemBeingPlaced);
			transform.localScale = Vector3.zero;
		}
	}
	private void ItemPlaced() {
		ItemPlacementDone();
		HouseItemManager.Instance.AddItem(itemBeingPlaced);
		itemBeingPlaced = null;
		Player.Instance.Wallet.TakeMoney(currentlySelectedItem.price);
	}
	private void CancelItemPlacement() {
		ItemPlacementDone();
		Destroy(itemBeingPlaced);
	}
	private void ItemPlacementDone() {
		transform.localScale = new Vector3(1f, 1f, 1f);
		ItemPlacementMode.Instance.confirmListeners -= ItemPlaced;
		ItemPlacementMode.Instance.cancelListeners -= CancelItemPlacement;
	}

	private void SetSelectedItem(ShopItem item) {
		selectedItemDetailsText.text = item.name;
		currentlySelectedItem = item;
	}

	private void Clear() {
		foreach(GameObject button in shopButtons) {
			Destroy(button);
		}
		shopButtons.Clear();
	}

	public void Show() {
		transform.localScale = new Vector3(1f, 1f, 1f);
		screenShown = true;

		SetShownItems(ShopItemCategory.FOR_CATS);
	}
	public void Hide() {
		transform.localScale = Vector3.zero;
		screenShown = false;
	}
}
