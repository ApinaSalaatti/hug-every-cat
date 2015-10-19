using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatstagramSendGUI : MonoBehaviour, InputReceiver {
	[SerializeField]
	private GameObject gui;
	[SerializeField]
	private Text infoText;
	[SerializeField]
	private Button sendButton;
	[SerializeField]
	private InputField descriptionField;
	[SerializeField]
	private InputField nameField;
	[SerializeField]
	private InputField findUrlField;
	[SerializeField]
	private CatstagramAPI catstagram;

	private Texture2D image;

	public void Reset() {
		infoText.text = "";
		infoText.gameObject.SetActive(false);
		sendButton.gameObject.SetActive(true);
		image = null;
		descriptionField.gameObject.SetActive(true);
		descriptionField.text = "";
		findUrlField.transform.parent.gameObject.SetActive(false);
		findUrlField.text = "";
	}

	public void SetImageToSend(Texture2D img) {
		image = img;
	}

	public void Open() {
		GlobalInput.Instance.AddInputReceiver(this);
		gui.SetActive(true);
	}

	public void Close() {
		GlobalInput.Instance.RemoveInputReceiver(this);
		gui.SetActive(false);
	}

	public bool ReceiveInput() {
		return true;
	}

	public void SendImage() {
		sendButton.gameObject.SetActive(false);
		descriptionField.gameObject.SetActive(false);
		infoText.gameObject.SetActive(true);
		infoText.text = "Sending image, please wait...";

		catstagram.imageSendDoneListeners += OnSendDone;
		catstagram.errorListeners += OnError;

		if(nameField != null) {
			catstagram.SendCatImage(image, descriptionField.text, nameField.text);
		}
		else {
			catstagram.SendCatImage(image, descriptionField.text);
		}
	}

	private void ClearListeners() {
		catstagram.imageSendDoneListeners -= OnSendDone;
		catstagram.errorListeners -= OnError;
	}

	private void OnSendDone(WWW response) {
		ClearListeners();

		try {
			JSONObject json = new JSONObject(response.text);
			Debug.Log(json.GetField("id").str);
			infoText.text = "Image sent!";
			findUrlField.transform.parent.gameObject.SetActive(true);
			string id = json.GetField("id").str;
			findUrlField.text = catstagram.FindImageUrl + id;
			AddToOwnCats(id);
		}
		catch(System.Exception) {
			OnError(response);
		}
	}
	private void OnError(WWW response) {
		ClearListeners();

		infoText.text = "Something went wrong. Check your Internet connection and try again!";
		sendButton.gameObject.SetActive(true);
	}

	private void AddToOwnCats(string id) {
		JSONObject json = null;

		// If the catstagram file exists, some cats have already been saved
		if(System.IO.File.Exists(Globals.SaveFolder + "catstagram")) {
			string[] lines = System.IO.File.ReadAllLines(Globals.SaveFolder + "catstagram");
			json = new JSONObject(lines[0]);
		}
		// Otherwise we must create an empty list of cats
		else {
			json = new JSONObject(JSONObject.Type.OBJECT);
			JSONObject catList = new JSONObject(JSONObject.Type.ARRAY);
			json.AddField("cats", catList);
		}

		// Add the id to the list of cats
		json.GetField("cats").Add(id);

		// Write to file
		using(System.IO.StreamWriter file = new System.IO.StreamWriter(Globals.SaveFolder + "catstagram")) {
			file.WriteLine(json.Print());
		}
	}
}
