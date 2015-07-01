using UnityEngine;
using System.Collections;

[System.Serializable]
public class SatisfiedNeed {
	public CatNeedType type;
	public float amountPerSecond;
}

/*
 * An item that can be placed in the house and used by the cats
 */
public class HouseItem : MonoBehaviour {
	[SerializeField]
	private string itemName;
	public string ItemName { get { return itemName; } }

	[SerializeField]
	private SatisfiedNeed[] satisfiedNeeds;
	private CatNeedType[] needTypes; // A list of need types this item satisfies, for quick access

	[SerializeField]
	private int simultaneouslyUsableBy;
	[SerializeField]
	private Transform[] usePositions;

	private bool[] positionInUse;
	private GameObject[] users;

	void Awake() {
		positionInUse = new bool[usePositions.Length];
		users = new GameObject[usePositions.Length];

		// Create the need types list
		needTypes = new CatNeedType[satisfiedNeeds.Length];
		for(int i = 0; i < needTypes.Length; i++) {
			needTypes[i] = satisfiedNeeds[i].type;
		}

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
			CatNeedType n = satisfiedNeeds[i].type;
			float amountToIncrease = satisfiedNeeds[i].amountPerSecond * deltaTime;
			user.SendMessage("NeedIncreased", new NeedIncreasedEffect(n, amountToIncrease));
		}
	}

	public Transform GetUsePosition() {
		for(int i = 0; i < positionInUse.Length; i++) {
			if(!positionInUse[i]) {
				return usePositions[i];
			}
		}

		return null;
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
		foreach(SatisfiedNeed s in satisfiedNeeds) {
			if(s.type.Equals(type))
				return true;
		}
		return false;
	}
	public virtual CatNeedType[] GetSatisfiedNeeds() {
		return needTypes;
	}

	public virtual void ItemAwake() { }
	public virtual void ItemUpdate(float deltaTime) { }

	public void CreateFromJSON(JSONObject json) {

	}
}
