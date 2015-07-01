using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InteractionMenu : MonoBehaviour {
	private static InteractionMenu instance;
	public static InteractionMenu Instance { get { return instance; } }

	[SerializeField]
	private GameObject buttonPrefab;
	[SerializeField]
	private Transform buttonParent;

	private GameObject associatedItem;

	private List<GameObject> buttons;

	private bool open = false;

	// Use this for initialization
	void Awake() {
		instance = this;
		buttons = new List<GameObject>();
	}

	void Update() {
		if(open) {
			SetPosition();
		}
	}

	private void SetPosition() {
		Vector3 pos = Camera.main.WorldToScreenPoint(associatedItem.transform.position);
		transform.position = pos;
	}

	private void Clear() {
		foreach(GameObject b in buttons) {
			Destroy(b);
		}
		buttons.Clear();
	}

	private void ItemSelected(InteractionItem i) {
		i.action();
		Close();
	}

	public void Open(Interactable i) {
		associatedItem = i.gameObject;
		open = true;
		SetPosition();

		transform.localScale = new Vector3(1f, 1f, 1f);
		Clear();
		foreach(InteractionItem item in i.Interactions) {
			GameObject b = Instantiate(buttonPrefab);
			b.transform.SetParent(buttonParent);
			b.GetComponentInChildren<Text>().text = item.text;
			InteractionItem it = item;
			b.GetComponent<Button>().onClick.AddListener(() => ItemSelected(it));
			buttons.Add(b);
		}
	}
	public void Close() {
		associatedItem = null;
		open = false;
		transform.localScale = Vector3.zero;
	}
}
