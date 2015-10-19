using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelInfo : MonoBehaviour {
	[SerializeField]
	private Text levelNameText;
	[SerializeField]
	private Text bestScoreText;

	public void SetLevel(string lvl) {
		levelNameText.text = lvl;

		bestScoreText.text = LevelManager.Instance.GetLevelScore(lvl).ToString();
	}
}
