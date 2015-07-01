using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ShopItemCategory { ALL, CAT_NEED, CAT_ACCESSORY }

public class ShopItem {
	public string resourceName;
	public string name;
	public string description;
	public Sprite image;
	public int price;
	public ShopItemCategory category;
}

// This is pretty much just a container for all the stuff that can be bought. 99% of the shopping magic happens in the ShopScreen
public class Shop : MonoBehaviour {
	private static Shop instance;
	public static Shop Instance { get { return instance; } }
	private List<ShopItem> allItems;

	// Items indexed by their category, for fast access
	private Dictionary<ShopItemCategory, List<ShopItem>> itemsByCategory;

	// Use this for initialization
	void Awake() {
		instance = this;

		allItems = new List<ShopItem>();
		itemsByCategory = new Dictionary<ShopItemCategory, List<ShopItem>>();

		ShopItem it = new ShopItem();
		it.resourceName = "food";
		it.name = "Food Bowl";
		it.description = "A basic food bowl for a pet";
		it.image = null;
		it.price = 10;
		it.category = ShopItemCategory.CAT_NEED;
		AddItem(it);
	}

	public void AddItem(ShopItem i) {
		allItems.Add(i);
		List<ShopItem> list;
		if(itemsByCategory.TryGetValue(i.category, out list)) {
			list.Add(i);
		}
		else {
			list = new List<ShopItem>();
			list.Add(i);
			itemsByCategory.Add(i.category, list);
		}
	}

	public List<ShopItem> GetItems(ShopItemCategory category = ShopItemCategory.ALL) {
		if(category == ShopItemCategory.ALL) {
			return allItems;
		}
		else {
			return itemsByCategory[category];
		}
	}
}
