using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerTrigger))]
public class EndZone : MonoBehaviour {
	[SerializeField]
	private string nextLevel;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PlayerEnteredVicinity(GameObject player) {
		//GameState.Instance.SetNextLevel(nextLevel);
		GameState.Instance.GameOver(GameOverMode.SUCCESS, nextLevel);
	}
}
