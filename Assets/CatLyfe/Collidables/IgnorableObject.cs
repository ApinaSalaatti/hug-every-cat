using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CollidableObject))]
public class IgnorableObject : MonoBehaviour {
	private enum PlayerIgnoreState { IN_VICINITY, USED, ALREADY_IGNORED }

	[SerializeField]
	private float playTime = 1f;
	[SerializeField]
	private ParticleSystem particles;

	private Dictionary<GameObject, PlayerIgnoreState> playerStates;

	// Use this for initialization
	void Awake() {
		playerStates = new Dictionary<GameObject, PlayerIgnoreState>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionWithPlayer(GameObject player) {
		particles.Stop();

		if(playerStates[player] == PlayerIgnoreState.IN_VICINITY) {
			// Add the info that player already used the item
			playerStates.Remove(player);
			playerStates.Add(player, PlayerIgnoreState.USED);

			// Force player to interact!
			StartCoroutine(PlaySequence(player));
		}
	}

	private IEnumerator PlaySequence(GameObject player) {
		player.GetComponent<Animator>().SetBool("PlayingWithToy", true);
		player.GetComponent<PlayerMover>().enabled = false;

		yield return new WaitForSeconds(playTime);

		player.GetComponent<PlayerMover>().enabled = true;
		player.GetComponent<Animator>().SetBool("PlayingWithToy", false);
	}


	// These two will be called by the player sensor
	void PlayerEnteredVicinity(GameObject player) {
		Debug.Log("Hello player!");
		if(!playerStates.ContainsKey(player)) {
			// The player entered the vicinity for the first time
			playerStates.Add(player, PlayerIgnoreState.IN_VICINITY);
			particles.Play();
		}
	}

	void PlayerExitedVicinity(GameObject player) {
		particles.Stop();

		PlayerIgnoreState state;
		if(playerStates.TryGetValue(player, out state)) {
			if(state == PlayerIgnoreState.IN_VICINITY) {
				// Item ignored, player gets score!
				CollidableObject col = GetComponent<CollidableObject>();
				player.SendMessage("AddScore", new ScoreEvent(col.ScoreValue, ScoreType.IGNORE, col.ScoreText, transform.position));
				// Change the status to ALREADY_IGNORED
				playerStates.Remove(player);
				playerStates.Add(player, PlayerIgnoreState.ALREADY_IGNORED);
			}
		}
		else {
			// SOMETHING IS WRONG!!
			Debug.Log("Player left but never entered the vicinity of " + gameObject.name + ". What is this?!");
		}
	}
}
