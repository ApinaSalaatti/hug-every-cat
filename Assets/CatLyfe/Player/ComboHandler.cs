using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboHandler : MonoBehaviour {
	private int multiplier;
	public int Multiplier { get { return multiplier; } }

	// How long has the current combo lasted
	private float comboLength = 0f;

	// The interval between scores that can elapse before combo resets
	private float comboResetTime = 5f;

	private float timer = 0f;

	private List<ScoreEvent> currentCombo;

	private int breaks;
	public int Breaks { get { return breaks; } }

	private int ignores;
	public int Ignores { get { return ignores; } }

	private int plays;
	public int Plays { get { return plays; } }

	private bool eclectic;
	public bool Eclectic { get { return eclectic; } }

	public int TotalCombo {
		get {
			int e = eclectic ? 1 : 0;
			return breaks + ignores + plays + e;
		}
	}

	// This keeps track of how many times the multiplier has been increased because of the total amount of the combo
	private int totalComboMultiplierAdditions;

	void Awake() {
		ResetCombo();
		currentCombo = new List<ScoreEvent>();
	}

	public void Update() {
		timer += Time.deltaTime;
		comboLength += Time.deltaTime;

		if(timer >= comboResetTime) {
			ResetCombo();
		}
	}

	private void ResetCombo() {
		comboLength = 0f;
		timer = 0f;
		multiplier = 1;

		breaks = 0;
		ignores = 0;
		plays = 0;

		eclectic = false;

		totalComboMultiplierAdditions = 0;
	}

	public void NewScore(ScoreEvent se) {
		timer = 0f;
		currentCombo.Add(se);

		switch(se.type) {
		case ScoreType.BREAK:
			breaks++;
			break;
		case ScoreType.IGNORE:
			ignores++;
			break;
		case ScoreType.PLAY:
			plays++;
			break;
		}

		if(!eclectic) {
			if(breaks > 0 && ignores > 0 && plays > 0) {
				// ECLECTIC CATTING!!
				eclectic = true;
				multiplier += 1;

				Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward * 15f;
				TextEffectSpawner.Instance.SpawnTextEffect("ECLECTIC!", pos, 2f).transform.SetParent(Camera.main.transform);
			}
		}

		if(((totalComboMultiplierAdditions+1) * 10) < TotalCombo) {
			totalComboMultiplierAdditions++;
			multiplier++;
		}
	}
}
