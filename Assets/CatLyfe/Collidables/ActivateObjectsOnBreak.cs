using UnityEngine;
using System.Collections;

public class ActivateObjectsOnBreak : MonoBehaviour {
	[SerializeField]
	private GameObject[] objects;

	void OnBreak() {
		for(int i = 0; i < objects.Length; i++) {
			objects[i].SetActive(true);
		}
	}
}
