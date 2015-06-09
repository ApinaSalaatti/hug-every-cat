using UnityEngine;
using System.Collections;

/*
 * Item usage effects
 */
public class NeedIncreasedEffect {
	public CatNeedType type;
	public float amount;
	public NeedIncreasedEffect(CatNeedType t, float a) {
		type =t ;
		amount = a;
	}
}

/*
 * An item that can be placed in the house and used by the cats
 */
public class HouseItem : MonoBehaviour {
	[SerializeField]
	private CatNeedType[] satisfiedNeeds; // The needs this item satisfies when used, if any
	[SerializeField]
	private float[] amountSatisfiedPerSecond;
	[SerializeField]
	private int simultaneouslyUsableBy;
	[SerializeField]
	private Transform[] usePositions;

	private bool[] positionInUse;
	private GameObject[] users;

	void Awake() {
		positionInUse = new bool[usePositions.Length];
		users = new GameObject[usePositions.Length];

		ItemAwake();
	}

	void WorldUpdate(float deltaTime) {
		ItemUpdate(deltaTime);

		// Apply the effect to all users
		for(int i = 0; i < positionInUse.Length; i++) {
			if(positionInUse[i])
				ApplyEffect(users[i], deltaTime);
		}
	}

	private void ApplyEffect(GameObject user, float deltaTime) {
		for(int i = 0; i < satisfiedNeeds.Length; i++) {
			CatNeedType n = satisfiedNeeds[i];
			float amountToIncrease = amountSatisfiedPerSecond[i] * deltaTime;
			user.SendMessage("NeedIncreased", new NeedIncreasedEffect(n, amountToIncrease));
		}
	}

	public bool StartUse(GameObject user) {
		int space = GetFreeSpace();
		if(space > -1) {
			positionInUse[space] = true;
			users[space] = user;
			return true;
		}

		return false;
	}
	public void StopUse(GameObject user) {
		for(int i = 0; i < users.Length; i++) {
			if(users[i] == user) {
				users[i] = null;
				positionInUse[i] = false;
			}
		}
	}

	// Returns -1 if no free space found
	private int GetFreeSpace() {
		for(int i = 0; i < positionInUse.Length; i++) {
			if(!positionInUse[i])
				return i;
		}
		return -1;
	}

	public bool HasSpace() {
		foreach(bool b in positionInUse) {
			if(!b) return true;
		}
		return false;
	}

	public virtual bool CanSatisfyNeed(CatNeedType type) {
		foreach(CatNeedType t in satisfiedNeeds) {
			if(t.Equals(type))
				return true;
		}
		return false;
	}

	public virtual void ItemAwake() { }
	public virtual void ItemUpdate(float deltaTime) { }
}
