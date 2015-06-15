using UnityEngine;
using System.Collections;

public class SelectionIndicator : MonoBehaviour {
	private static GameObject indicatorObject;

	private static Renderer rendererToSize;
	private static GameObject selectedObject;
	private static float baseSize; // Used when scaling the indicator

	private RectTransform rTransform;

	// Use this for initialization
	void Awake() {
		indicatorObject = gameObject;
		rTransform = GetComponent<RectTransform>();
		indicatorObject.SetActive(false);

		float ratio = Screen.width / 615f;
		baseSize = 60f * ratio; // This seems to provide good results for the size of the indicator
	}
	
	// Update is called once per frame
	void Update () {
		if(selectedObject != null) {
			Vector3 screenPos = Camera.main.WorldToScreenPoint(selectedObject.transform.position);
			Vector3 size = rendererToSize.bounds.size;
			size *= baseSize;

			rTransform.sizeDelta = size;
			transform.position = screenPos;
		}
	}

	public static void SetSelected(GameObject s, Renderer r) {
		if(s == null || r == null) {
			Hide();
			return;
		}

		rendererToSize = r;
		indicatorObject.SetActive(true);
		selectedObject = s;
	}
	public static void Hide() {
		indicatorObject.SetActive(false);
	}
}
