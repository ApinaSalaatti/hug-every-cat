using UnityEngine;
using System.Collections;

public class ActivateObjectsOnCollision : MonoBehaviour {
	[SerializeField]
	private GameObject[] objects;
	
	void OnCollisionWithPlayer() {
		for(int i = 0; i < objects.Length; i++) {
			objects[i].SetActive(true);
		}
	}
}
