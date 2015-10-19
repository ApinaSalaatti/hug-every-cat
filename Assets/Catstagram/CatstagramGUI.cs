using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatstagramGUI : MonoBehaviour {
	[SerializeField]
	private CatstagramAPI catstagram;

	[SerializeField]
	private GameObject imageListParent;
	[SerializeField]
	private Text imageListText;

	[SerializeField]
	private GameObject imageDisplayPrefab;

	private string[] ownCatIDs;

	void Start() {
		ShowRandomImages();
		GetOwnCatIDs();
	}

	private void GetOwnCatIDs() {
		if(System.IO.File.Exists(Globals.SaveFolder + "catstagram")) {
			string[] lines = System.IO.File.ReadAllLines(Globals.SaveFolder + "catstagram");
			JSONObject json = new JSONObject(lines[0]);
			JSONObject cats = json.GetField("cats");
			ownCatIDs = new string[cats.Count];
			for(int i = 0; i < ownCatIDs.Length; i++) {
				ownCatIDs[i] = cats.list[i].str;
			}
		}
		else {
			ownCatIDs = new string[0];
		}
	}

	public void Clear() {
		for(int i = 0; i < imageListParent.transform.childCount; i++) {
			Destroy(imageListParent.transform.GetChild(i).gameObject);
		}
	}

	public void ShowRandomImages() {
		Clear();
		catstagram.randomImagesGetListener += ImagesFetched;
		catstagram.GetRandomImages();
	}
	private void ImagesFetched(WWW response) {
		catstagram.randomImagesGetListener -= ImagesFetched;
		try {
			//Debug.Log(response.text);
			JSONObject json = new JSONObject(response.text);
			for(int i = 0; i < json.list.Count; i++) {
				JSONObject js = json.list[i];
				CreateCatstagramImage(js);
			}
		}
		catch(System.Exception) {
			OnError(response);
		}
	}

	public void ShowMyImages() {
		Clear();
		for(int i = 0; i < ownCatIDs.Length; i++) {
			catstagram.GetImage(ownCatIDs[i], ImageFetched);
		}
	}

	public void ShowImage(string id) {
		Clear();
		catstagram.GetImage(id, ImageFetched);
	}
	private void ImageFetched(WWW response) {
		JSONObject json = new JSONObject(response.text);
		CreateCatstagramImage(json);
	}

	private void CreateCatstagramImage(JSONObject json) {
		string id = json.GetField("id").str;
		GameObject img = Instantiate(imageDisplayPrefab) as GameObject;
		img.transform.SetParent(imageListParent.transform);
		
		catstagram.GetImageData(id, (WWW www) => {
			img.GetComponent<CatstagramImage>().SetCatstagram(catstagram);
			img.GetComponent<CatstagramImage>().SetCat(json, www);
		});
	}

	public void BackToMainMenu() {
		Application.LoadLevel(1);
	}

	private void OnError(WWW response) {

	}
}
