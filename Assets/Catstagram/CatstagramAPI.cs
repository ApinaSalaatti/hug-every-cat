using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Security.Cryptography;

public class CatstagramAPI : MonoBehaviour {
	private string secretKey = "";
	private string sendImageUrl = "http://skeidaa.com/catstagram/backend/add";
	private string findImageUrl = "http://skeidaa.com/catstagram/#/";

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

	private Texture2D image;
		
	public void SendCatImage(Texture2D img) {
		image = img;
		gui.SetActive(true);
	}
	public void Close() {
		gui.SetActive(false);
	}

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

	public void StartImageSend() {
		StartCoroutine(Send(image, descriptionField.text, nameField.text));
	}
	private IEnumerator Send(Texture2D image, string desc = "", string catName = "Unnamed Cat") {
		Debug.Log("Sending...");

		sendButton.gameObject.SetActive(false);
		descriptionField.gameObject.SetActive(false);
		infoText.gameObject.SetActive(true);
		infoText.text = "Sending image, please wait...";

		byte[] bytes = image.EncodeToPNG();

		using(MD5 md5 = MD5.Create()) {
			byte[] data = md5.ComputeHash(bytes);

			string ret = "";
			for (int i=0; i < data.Length; i++)
				ret += data [i].ToString("x2").ToLower();

			//Debug.Log("Just the file: " + ret);
			
			byte[] newData = System.Text.Encoding.UTF8.GetBytes(ret + secretKey);
			newData = md5.ComputeHash(newData);
			ret = "";
			for (int i=0; i < newData.Length; i++)
				ret += newData [i].ToString("x2").ToLower();
			
			//Debug.Log("possibility 1: " + ret);

			WWWForm form = new WWWForm();
			form.AddField("key", ret, System.Text.Encoding.UTF8);
			form.AddField("name", catName, System.Text.Encoding.UTF8);
			form.AddField("description", desc, System.Text.Encoding.UTF8);
			form.AddBinaryData("image", bytes);

			WWW www = new WWW(sendImageUrl, form);
			yield return www;
			Debug.Log(www.text);
			try {
				JSONObject json = new JSONObject(www.text);
				Debug.Log(json.GetField("id").str);
				infoText.text = "Image sent!";
				findUrlField.transform.parent.gameObject.SetActive(true);
				findUrlField.text = findImageUrl + json.GetField("id").str;
			}
			catch(System.Exception) {
				infoText.text = "Something went wrong. Check your Internet connection and try again!";
				sendButton.gameObject.SetActive(true);
			}
		}
	}
}
