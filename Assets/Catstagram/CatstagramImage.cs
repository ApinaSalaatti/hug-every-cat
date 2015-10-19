using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatstagramImage : MonoBehaviour {
	[SerializeField]
	private CatstagramAPI catstagram;

	[SerializeField]
	private Image catImage;

	[SerializeField]
	private Text nameText;
	[SerializeField]
	private Text descriptionText;

	[SerializeField]
	private GameObject likeButton;
	[SerializeField]
	private GameObject unlikeButton;
	[SerializeField]
	private Text likeCount;

	private string id;

	public void SetCatstagram(CatstagramAPI c) {
		catstagram = c;
	}

	public void SetCat(JSONObject catData, WWW wwwImageData) {
		id = catData.GetField("id").str;

		string name = catData.GetField("name").str;
		string description = catData.GetField("description").str;
		string likes = catData.GetField("likes").str;
		Sprite s = catImage.sprite;

		Debug.Log("Setting image for " + name +" (id: " + id + ")");
		Texture2D tex = new Texture2D(500, 500, s.texture.format, false);
		tex.filterMode = FilterMode.Point;
		Sprite spr = Sprite.Create(tex, s.rect, new Vector2(0.5f, 0.5f), s.pixelsPerUnit);
		catImage.sprite = spr;

		wwwImageData.LoadImageIntoTexture(tex);

		nameText.text = name;
		descriptionText.text = description;
		likeCount.text = likes;

		if(catData.HasField("alreadyLiked") && catData.GetField("alreadyLiked").b) {
			likeButton.SetActive(false);
			unlikeButton.SetActive(true);
		}
		else {
			likeButton.SetActive(true);
			unlikeButton.SetActive(false);
		}
	}

	public void Like() {
		catstagram.SendLike(id);
		int likes = int.Parse(likeCount.text);
		likes++;
		likeCount.text = likes.ToString();
		likeButton.SetActive(false);
		unlikeButton.SetActive(true);
	}
	public void Unlike() {
		Debug.Log(catstagram);
		catstagram.SendUnlike(id);
		int likes = int.Parse(likeCount.text);
		likes--;
		likeCount.text = likes.ToString();
		likeButton.SetActive(true);
		unlikeButton.SetActive(false);
	}
}
