using UnityEngine;
using System.Collections;

public abstract class Achievement {
	public abstract string Name();
	public abstract string Description();
	public abstract bool IsAchieved();
}

public class AwesomeComboAchievement : Achievement {
	public override string Name ()
	{
		return "Combo Master";
	}

	public override string Description ()
	{
		return "Got a combo of length 30";
	}

	public override bool IsAchieved ()
	{
		return false;
	}

}