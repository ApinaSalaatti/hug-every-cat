using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CollidableObject))]
public class PlayableObject : MonoBehaviour {
	[SerializeField]
	private bool forcePosition = false;
	[SerializeField]
	private Transform position;
	[SerializeField]
	private string animationToPlay = "PlayingWithToy";

	private List<GameObject> playersAlreadyUsed;

	[SerializeField]
	private float playTime = 1f;

	// Use this for initialization
	void Start () {
		playersAlreadyUsed = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionWithPlayer(GameObject player) {
		if(!playersAlreadyUsed.Contains(player)) {
			playersAlreadyUsed.Add(player);

			if(forcePosition) {
				player.transform.position = position.transform.position;
			}

			// Make player play with object!
			StartCoroutine(PlaySequence(player));

			CollidableObject col = GetComponent<CollidableObject>();
			player.SendMessage("AddScore", new ScoreEvent(col.ScoreValue, ScoreType.PLAY, col.ScoreText, gameObject.transform.position));
		}
	}

	private IEnumerator PlaySequence(GameObject player) {
		if(animationToPlay != "") player.GetComponent<Animator>().SetBool(animationToPlay, true);
		player.GetComponent<PlayerMover>().enabled = false;

		yield return new WaitForSeconds(playTime);
		
		player.GetComponent<PlayerMover>().enabled = true;
		if(animationToPlay != "") player.GetComponent<Animator>().SetBool(animationToPlay, false);
	}
}
