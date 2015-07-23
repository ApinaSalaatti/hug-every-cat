using UnityEngine;
using System.Collections;

public class CameraItem : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Interactable i = gameObject.AddComponent<Interactable>();
		i.AddInteraction(new InteractionItem("Take Photo", OpenCamera));
	}
	
	private void OpenCamera() {
		HomeGUI.Instance.Hide();
		Photography.Instance.Show();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnAddedToWorld() {
		
	}
	
	void OnRemovedFromWorld() {
		
	}
	
	void Save(JSONObject json) {
		//JSONObject sc = json.GetField("specialComponents");
		//sc.Add("camera");
	}
	void Load(JSONObject json) {
		
	}
}
