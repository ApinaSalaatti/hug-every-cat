using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatOfTheDayDisplay : MonoBehaviour {
	[SerializeField]
	private CatstagramAPI catstagram;

	// Use this for initialization
	void Start () {
		catstagram.GetCatOfTheDay(DataFeched);	
	}

	private void DataFeched(WWW response) {
		try {
			JSONObject json = new JSONObject(response.text);
			string id = json.GetField("id").str;
			catstagram.GetImageData(id, (WWW www) => {
				GetComponent<CatstagramImage>().SetCat(json, www);
			});
		}
		catch(System.Exception) {

		}
	}
}
