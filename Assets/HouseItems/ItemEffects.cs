using UnityEngine;
using System.Collections;

/*
 * Positive item usage effects
 */
public class NeedIncreasedEffect {
	public CatNeedType type;
	public float amount;
	public NeedIncreasedEffect(CatNeedType t, float a) {
		type =t ;
		amount = a;
	}
}
