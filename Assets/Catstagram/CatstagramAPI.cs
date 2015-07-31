using UnityEngine;
using System.Collections;
using System.IO;
using System.Security.Cryptography;

public class CatstagramAPI : MonoBehaviour {
	private string secretKey = "";
	private string sendImageUrl = "http://skeidaa.com/catstagram/backend/add";

	public void SendCatImage(Texture2D image) {
		StartCoroutine(Send(image));
	}

	private IEnumerator Send(Texture2D image) {
		Debug.Log("Sending...");
		byte[] bytes = image.EncodeToPNG();

		using(MD5 md5 = MD5.Create()) {
			byte[] data = md5.ComputeHash(bytes);

			string ret = "";
			for (int i=0; i < data.Length; i++)
				ret += data [i].ToString("x2").ToLower();

			Debug.Log("Just the file: " + ret);
			
			byte[] newData = System.Text.Encoding.UTF8.GetBytes(ret + secretKey);
			newData = md5.ComputeHash(newData);
			ret = "";
			for (int i=0; i < newData.Length; i++)
				ret += newData [i].ToString("x2").ToLower();
			
			Debug.Log("possibility 1: " + ret);

			WWWForm form = new WWWForm();
			form.AddField("key", ret, System.Text.Encoding.UTF8);
			form.AddBinaryData("image", bytes);

			WWW www = new WWW(sendImageUrl, form);
			yield return www;
			Debug.Log(www.text);
			JSONObject json = new JSONObject(www.text);
			Debug.Log(json.GetField("id").str);
		}
	}
}
