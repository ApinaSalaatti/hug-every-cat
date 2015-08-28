using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Security.Cryptography;

public class CatstagramAPI : MonoBehaviour {
	private string secretKey = "";
	private string userGUID;

	private string getRandomImagesUrl = "http://skeidaa.com/catstagram/backend/";
	private string getCatOfTheDayUrl = "http://skeidaa.com/catstagram/backend/catoftheday";

	private string sendImageUrl = "http://skeidaa.com/catstagram/backend/add/";
	public string SendImageUrl {
		get { return sendImageUrl; }
	}

	private string findImageUrl = "http://skeidaa.com/catstagram/backend/cat/";
	public string FindImageUrl {
		get { return findImageUrl; }
	}

	private string getImageDataUrl = "http://skeidaa.com/catstagram/backend/images/";

	private string sendLikeUrl = "http://skeidaa.com/catstagram/backend/cat/like/";
	public string SendLikeUrl {
		get { return sendLikeUrl; }
	}
	private string sendUnlikeUrl = "http://skeidaa.com/catstagram/backend/cat/unlike/";
	public string SendUnlikeUrl {
		get { return sendUnlikeUrl; }
	}

	public delegate void CatstagramEventListener(WWW response);
	public CatstagramEventListener imageSendDoneListeners;
	public CatstagramEventListener randomImagesGetListener;
	public CatstagramEventListener errorListeners;

	public void SetGUID() {
		if(File.Exists(Globals.SaveFolder + "userGUID")) {
			string[] lines = System.IO.File.ReadAllLines(Globals.SaveFolder + "userGUID");
			userGUID = lines[0];
		}
		else {
			// Create a new guid, set it and save it!
			System.Guid guid = System.Guid.NewGuid();
			userGUID = guid.ToString().ToUpper();
			using(System.IO.StreamWriter file = new System.IO.StreamWriter(Globals.SaveFolder + "userGUID")) {
				file.WriteLine(userGUID);
			}
		}
		Debug.Log("GUID: " + userGUID);
	}

	private IEnumerator Connect(string url, WWWForm data, CatstagramEventListener listener, bool addUserID = true) {
		if(userGUID == null || userGUID == "") {
			SetGUID();
		}

		// When sending data, we must provide the userID and the userID hashed
		if(addUserID) {
			if(data != null) {
				string hashedID = "";
				using(MD5 md5 = MD5.Create()) {
					byte[] bytes = System.Text.Encoding.UTF8.GetBytes(userGUID + secretKey);
					bytes = md5.ComputeHash(bytes);
					for (int i=0; i < bytes.Length; i++)
						hashedID += bytes[i].ToString("x2").ToLower();
				}
				data.AddField("userID", userGUID, System.Text.Encoding.UTF8);
				data.AddField("key", hashedID, System.Text.Encoding.UTF8);
			}
			else {
				// Else just append the userID as a GET parameter
				url += "?userID=" + userGUID;
			}
		}

		Debug.Log("Connecting...");
		Debug.Log("URL: " + url);
		if(data != null) {
			// This sends a POST request
			WWW www = new WWW(url, data);
			yield return www;
			if(listener != null) listener(www);
		}
		else {
			// This sends a GET request
			WWW www = new WWW(url);
			yield return www;
			if(listener != null) listener(www);
		}
	}

	public void SendCatImage(Texture2D image, string desc = "", string catName = "Unnamed Cat") {
		Debug.Log("Sending...");

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

			StartCoroutine(Connect(sendImageUrl, form, imageSendDoneListeners, false));
		}
	}

	public void SendLike(string id) {
		Debug.Log("Sending like for ID: " + id);
		WWWForm data = new WWWForm();
		data.AddField("image", id, System.Text.Encoding.UTF8);

		StartCoroutine(Connect(sendLikeUrl, data, null));
	}

	public void SendUnlike(string id) {
		Debug.Log("Sending unlike for ID: " + id);
		WWWForm data = new WWWForm();
		data.AddField("image", id, System.Text.Encoding.UTF8);

		StartCoroutine(Connect(sendUnlikeUrl, data, null));
	}

	public void GetRandomImages() {
		Debug.Log("Getting random images");
		StartCoroutine(Connect(getRandomImagesUrl, null, randomImagesGetListener));
	}

	public void GetCatOfTheDay(CatstagramEventListener listener) {
		StartCoroutine(Connect(getCatOfTheDayUrl, null, listener));
	}

	public void GetImage(string id, CatstagramEventListener listener) {
		StartCoroutine(Connect(findImageUrl + id, null, listener));
	}

	public void GetImageData(string id, CatstagramEventListener listener) {
		StartCoroutine(Connect(getImageDataUrl + id, null, listener));
	}
}
