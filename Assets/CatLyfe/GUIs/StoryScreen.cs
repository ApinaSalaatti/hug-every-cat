using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum StoryCharacter { MYSTERY, CATTON, EVIL_AI }

public class StoryScreen : MonoBehaviour, InputReceiver {
	private static StoryScreen instance;
	public static StoryScreen Instance { get { return instance; } }

	public const string mysteryName = "Mystery Life Form";
	public const string cattonName = "Sgt. Catton Oswald";
	public const string evilAIName = "Hugecorp CEO";

	[SerializeField]
	private Text storyText;
	[SerializeField]
	private Text nameText;
	[SerializeField]
	private Image callerImage;

	[SerializeField]
	private Sprite mysterySprite;
	[SerializeField]
	private Sprite cattonSprite;
	[SerializeField]
	private Sprite evilAISprite;

	// Use this for initialization
	void Awake() {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool ReceiveInput() {
		if(Input.GetButtonDown("Cancel")) {
			Hide();
		}

		return true;
	}

	public void Hide() {
		transform.localScale = Vector3.zero;
		GlobalInput.Instance.RemoveInputReceiver(this);
	}

	public void ShowStory(string story, StoryCharacter whoIsTalking) {
		transform.localScale = new Vector3(1f, 1f, 1f);
		GlobalInput.Instance.AddInputReceiver(this);
		storyText.text = story;

		switch(whoIsTalking) {
		case StoryCharacter.MYSTERY:
			nameText.text = mysteryName;
			callerImage.sprite = mysterySprite;
			break;
		case StoryCharacter.CATTON:
			nameText.text = cattonName;
			callerImage.sprite = cattonSprite;
			break;
		case StoryCharacter.EVIL_AI:
			nameText.text = evilAIName;
			callerImage.sprite = evilAISprite;
			break;
		}
	}
}
