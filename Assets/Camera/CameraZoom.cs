using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {
	[SerializeField]
	private float[] zoomLevelCameraSizes;

	[SerializeField]
	private int zoomLevel = 1; // 1 == the basic zoom level, i.e. camera size 5

	void Start() {
		SetZoom();
	}

	public void ZoomIn() {
		if(zoomLevel < 2) {
			zoomLevel++;
			SetZoom();
		}
	}

	public void ZoomOut() {
		if(zoomLevel > 0) {
			zoomLevel--;
			SetZoom();
		}
	}

	private void SetZoom() {
		Camera.main.orthographicSize = zoomLevelCameraSizes[zoomLevel];
	}
}
