using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerComboDisplay : MonoBehaviour {
	[SerializeField]
	private PlayerScore score;

	[SerializeField]
	private Text totalComboText;

	[SerializeField]
	private Text comboText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		totalComboText.text = "";
		comboText.text = "";
		if(score.Combos.TotalCombo > 10) {
			totalComboText.text = "Current combo: x" + score.Combos.TotalCombo + " (multiplier: " + score.Combos.Multiplier + ")";

			if(score.Combos.Breaks > 0) {
				comboText.text += "- Broken Important Object x" + score.Combos.Breaks + "\n";
			}
			if(score.Combos.Ignores > 0) {
				comboText.text += "- Ignored Expensive Toy x" + score.Combos.Ignores + "\n";
			}
			if(score.Combos.Plays > 0) {
				comboText.text += "- Played With Non-Toy x" + score.Combos.Plays + "\n";
			}
			if(score.Combos.Eclectic) {
				comboText.text += "- Eclectic catting!" + "\n";
			}
		}
	}
}
