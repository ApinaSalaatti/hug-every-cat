using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void InteractionItemAction();

public class InteractionItem {
	public string text;
	public InteractionItemAction action;

	public InteractionItem(string t, InteractionItemAction a) {
		text = t;
		action = a;
	}
}

public class Interactable : MonoBehaviour {
	private List<InteractionItem> interactions;
	public InteractionItem[] Interactions { get { return interactions.ToArray(); } }

	void Awake() {
		interactions = new List<InteractionItem>();
	}

	public void ClearInteractions() {
		interactions.Clear();
	}
	public void SetInteractions(InteractionItem[] items) {
		ClearInteractions();
		interactions.AddRange(items);
	}
	public void AddInteraction(InteractionItem i) {
		interactions.Add(i);
	}
	public void RemoveInteraction(InteractionItem i) {
		interactions.Remove(i);
	}

	void OnSelect() {
		InteractionMenu.Instance.Open(this);
	}
	
	void OnDeselect() {
		InteractionMenu.Instance.Close();
	}
	
	void OnDrag() {
		InteractionMenu.Instance.Close();
	}
}
