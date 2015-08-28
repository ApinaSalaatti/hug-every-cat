using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScoreDisplay : MonoBehaviour {
	[SerializeField]
	private Text nameText;
	[SerializeField]
	private Text scoreText;

	[SerializeField]
	private CatStats playerInfo;
	[SerializeField]
	private PlayerScore player;

	// Use this for initialization
	void Start () {
		SetPlayerName(playerInfo.Name);
	}

	public void SetPlayerName(string name) {
		nameText.text = name + "'s Score:";
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = player.Score.ToString();
	}
}
