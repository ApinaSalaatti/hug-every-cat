using UnityEngine;
using System.Collections;

// Agility can help the cat in its adventures
// Strength can help the cat in its adventures
// Smarts allow the cat to research
// Friendliness allows the cat to befriend new cats
// Curiosity allows the cat to adventure longer
// Fighter cats can go to territories with enemy cats to fight them off
public enum SkillType { STRENGTH, AGILITY, SMARTS, FRIENDLINESS, CURIOSITY, FIGHTER }

// Skills affect what the cat can do. One cat can only have one skill
// Skills gain XP when they are used
public class CatSkill {
	private SkillType skillType;
	public SkillType SkillType {
		get { return SkillType; }
	}

	private int xp = 0;
	public int XP { get { return xp; } }
	public void GainXP(int amount) {
		xp += amount;
	}

	public CatSkill(SkillType type) {
		skillType = type;
	}
}
