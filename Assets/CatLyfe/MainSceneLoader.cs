using UnityEngine;
using System.Collections;

public class MainSceneLoader : MonoBehaviour {
	public static int sceneToLoad = 7;
	public static string currentLevelName = "Back Yard";

	[SerializeField]
	private GameObject loadingScreen;

	// Use this for initialization
	void Start() {
		Application.LoadLevelAdditive(sceneToLoad);
		loadingScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
