using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// SCORE & COMBO IDEAS:
// - height bonus, stuff that's placed high (like at the top of cupboards) nets more points
// - chase bonus, get more points when being chased
// - combo timer, break lots of stuff fast to earn combos
// - eclectic catting bonus, lots of different actions in a small time window

// The HUMAN follows very fast but gets lost instantly when it loses sight of the cat

//- oksu matolle
//- tavaroita rikki
//- leiki pahvilaatikolla
//- ignore/hukkaa kalliit lelut

public enum ScoreType { BREAK, IGNORE, PLAY }

public class ScoreEvent {
	public int score;
	public ScoreType type;
	public string scoreEvent;
	public Vector3 position;

	public ScoreEvent(int s, ScoreType t, string se, Vector3 pos) {
		score = s;
		type = t;
		scoreEvent = se;
		position = new Vector3(pos.x, pos.y, pos.z);
	}
}

public class PlayerScore : MonoBehaviour {
	private int score;
	public int Score { get { return score; } }

	private ComboHandler combos;
	public ComboHandler Combos { get { return combos; } }

	[SerializeField]
	private ScoreEventDisplay scoreDisplay;

	private bool heightBonus = false;
	private bool chaseBonus = false;

	// Use this for initialization
	void Awake() {
		score = 0;
		combos = GetComponent<ComboHandler>();
	}
	
	// Update is called once per frame
	void Update () {
		heightBonus = transform.position.y > 6f;
		chaseBonus = HumanAlertness.Instance.Chasing;

		combos.Update();
	}

	private int GetBonuses() {
		int bonus = 0;

		if(heightBonus) {
			// We are so high!! High times bonusssss!
			bonus += 50;
		}

		if(chaseBonus) {
			// Chase bonus!!
			bonus += 200;
		}

		return bonus;
	}
	private string[] GetBonusTexts() {
		List<string> list = new List<string>();
		if(heightBonus)
			list.Add("Height Bonus 50pts");
		if(chaseBonus)
			list.Add("Chase Bonus 200pts");

		return list.ToArray();
	}

	void AddScore(ScoreEvent se) {
		// Send the score to the combo/multiplier calculator
		combos.NewScore(se);

		int totalScore = (se.score + GetBonuses()) * combos.Multiplier;

		score += totalScore;
		ParticleSpawner.Instance.SpawnItemScoredParticles(se.position);

		Color c = new Color(Random.value, Random.value, Random.value);
		TextEffectSpawner.Instance.SpawnTextEffect(se.scoreEvent, se.position, c, 1f);

		scoreDisplay.ShowEvent(se.scoreEvent, GetBonusTexts(), se.score, totalScore, combos.Multiplier);
	}
}
