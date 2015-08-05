using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class Photography : MonoBehaviour {
	private static Photography instance;
	public static Photography Instance { get { return instance; } }

	public delegate void PhotoDoneListener();
	public PhotoDoneListener listeners;

	[SerializeField]
	private GameObject cameraButton;
	[SerializeField]
	private GameObject catstagramButton;

	[SerializeField]
	private Image destinationImage;
	[SerializeField]
	private GameObject polaroid;

	[SerializeField]
	private CatstagramAPI catstagram;

	// Use this for initialization
	void Awake() {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakePhoto() {
		StartCoroutine(Photo());
	}

	private IEnumerator Photo() {
		Hide();
		yield return new WaitForEndOfFrame();

		Texture2D tex = new Texture2D(500, 500);
		float centerX = Screen.width / 2f;
		float centerY = Screen.height / 2f;
		Rect r = new Rect(centerX-250, centerY-250, 500, 500);
		tex.ReadPixels(r, 0, 0);
		tex.Apply();
		destinationImage.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

		if(listeners != null) listeners();

		//catstagramButton.SetActive(true);
		catstagram.Reset();
		ShowPreview();
		Show();
	}

	public void SavePhoto() {
		byte[] bytes = destinationImage.sprite.texture.EncodeToPNG();
		using(FileStream file = File.Open("catshot.png", FileMode.OpenOrCreate, FileAccess.Write)) {
			using(BinaryWriter bw = new BinaryWriter(file)) {
				bw.Write(bytes);
			}
		}
	}

	public void SendToCatstagram() {
		Debug.Log("Let's send the image!");
		//catstagramButton.SetActive(false);
		catstagram.SendCatImage(destinationImage.sprite.texture);
	}

	public void ShowPreview() {
		polaroid.SetActive(true);
	}
	public void ClosePreview() {
		polaroid.SetActive(false);
	}

	public void Show() {
		transform.localScale = new Vector3(1f, 1f, 1f);
	}
	public void Hide() {
		transform.localScale = Vector3.zero;
	}

	public void Enable() {
		cameraButton.SetActive(true);
	}
	public void Disable() {
		cameraButton.SetActive(false);
	}
}
