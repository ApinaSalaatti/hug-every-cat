using UnityEngine;
using System.Collections;

public enum BuffType { INSTANT, OVER_TIME }

[System.Serializable]
public class Buff {
	[SerializeField]
	private BuffType buffType;
	public BuffType BuffType { get { return buffType; } }

	[SerializeField]
	private int amount;

	public Buff() {
		// Empty :(
	}

	public Buff(BuffType t, int a) {
		buffType = t;
		amount = a;
	}
}

public class NeedBuff : Buff {

}

public class SkillBuff : Buff {

}

public class StatBuff : Buff {

}