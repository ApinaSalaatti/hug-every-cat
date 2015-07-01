using UnityEngine;
using System.Collections;

public class SelectionIndicator : MonoBehaviour {
	private static SelectionIndicator instance;
	public static SelectionIndicator Instance { get { return instance; } }

	private Renderer rendererToSize;
	private GameObject selectedObject;
	private float baseSize; // Used when scaling the indicator

	private RectTransform rTransform;

	// Use this for initialization
	void Awake() {
		instance = this;
		rTransform = GetComponent<RectTransform>();
		gameObject.SetActive(false);

		// This seems to provide good results for the size of the indicator
		float ratio = Screen.width / 615f;
		baseSize = 60f * ratio;
	}
	
	// Update is called once per frame
	void Update () {
		if(selectedObject != null) {
			MoveToSelection();
		}
	}

	private void MoveToSelection() {
		Vector3 screenPos = Camera.main.WorldToScreenPoint(selectedObject.transform.position);
		Vector3 size = rendererToSize.bounds.size;
		size *= baseSize;
		
		rTransform.sizeDelta = size;
		transform.position = screenPos;
	}

	public void SetSelected(GameObject s, Renderer r) {
		if(s == null || r == null) {
			Hide();
			return;
		}
		selectedObject = s;
		rendererToSize = r;

		MoveToSelection();
		gameObject.SetActive(true);
	}
	public void Hide() {
		selectedObject = null;
		gameObject.SetActive(false);
	}
}
