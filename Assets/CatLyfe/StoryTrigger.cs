using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerTrigger))]
public class StoryTrigger : MonoBehaviour {
	[SerializeField]
	private string story = "Hey!";
	[SerializeField]
	private StoryCharacter character = StoryCharacter.CATTON;

	private bool storyShown = false;

	void PlayerEnteredVicinity(GameObject player) {
		if(!storyShown) {
			storyShown = true;
			StoryScreen.Instance.ShowStory(story, character);
		}
	}
}
