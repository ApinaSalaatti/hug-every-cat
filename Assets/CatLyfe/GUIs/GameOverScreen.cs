using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScreen : MonoBehaviour {
	[SerializeField]
	private GameObject player;

	[SerializeField]
	private Text nameText;
	[SerializeField]
	private Text scoreText;
	[SerializeField]
	private Text titleText;

	[SerializeField]
	private Text successText;
	[SerializeField]
	private GameObject nextLevelButton;

	public void SetPlayer(GameObject p) {
		player = p;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show(GameOverMode mode) {
		transform.localScale = new Vector3(1f, 1f, 1f);

		CatStats stats = player.GetComponent<CatStats>();
		PlayerScore score = player.GetComponent<PlayerScore>();

		nameText.text = stats.Name;
		scoreText.text = score.Score.ToString() + " points!";

		titleText.text = GiveTitle(score.Score);

		if(mode == GameOverMode.SUCCESS) {
			successText.text = "Level Cleared!";
			nextLevelButton.SetActive(true);
		}
		else if(mode == GameOverMode.FORFEIT) {
			successText.text = "Level Forfeited!";
		}
	}

	private string GiveTitle(int score) {
		return "Master Smasher";
	}
}
