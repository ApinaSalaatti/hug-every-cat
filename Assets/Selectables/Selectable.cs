using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {
	// The renderer to use when determining the size of the selection
	[SerializeField]
	private Renderer renderer;

	[SerializeField]
	private DetailType detailType;

	void OnSelect() {
		SelectionIndicator.SetSelected(gameObject, renderer);
		SelectedObjectDetails.Instance.ShowDetails(gameObject, detailType);
	}

	void OnDeselect() {
		SelectionIndicator.Hide();
		SelectedObjectDetails.Instance.HideDetails();
	}
}
