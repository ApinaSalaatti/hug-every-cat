using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {
	// The renderer to use when determining the size of the selection
	[SerializeField]
	private new Renderer renderer;

	[SerializeField]
	private DetailType detailType;

	void OnSelect() {
		SelectionIndicator.Instance.SetSelected(gameObject, renderer);
		SelectedObjectDetails.Instance.ShowDetails(gameObject, detailType);
	}

	void OnDeselect() {
		SelectionIndicator.Instance.Hide();
		SelectedObjectDetails.Instance.HideDetails();
	}
}
