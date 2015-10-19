using UnityEngine;
using System.Collections;

public class HumanAlertness : MonoBehaviour {
	[SerializeField]
	private GameObject handPrefab;

	[SerializeField]
	private GameObject player;

	[SerializeField]
	private FollowingHand hand;

	private static HumanAlertness instance;
	public static HumanAlertness Instance { get { return instance; } }

	private float currentAlertness = 0f;
	public float CurrentAlertness { get { return currentAlertness; } }

	// Alertness level changes when current alertness reaches this amount
	private float maxAlertness = 30f;
	public float MaxAlertness { get { return maxAlertness; } }

	private int alertnessLevel = 0;
	public int AlertnessLevel { get { return alertnessLevel; } }

	private float timeFromLastNoise = 0f;
	private float timeUntilAlertnessDecay = 1f; // The time that has to elapse from a heard noise until the alertness starts to decay

	// This is the amount the alertness lessens per second
	private float alertnessDecayRate = 1f;

	private bool chasing = false;
	public bool Chasing { get { return chasing; } }

	void Awake() {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		timeFromLastNoise += Time.deltaTime;
		if(chasing) {
			// Reduce the alertness all the time when chasing, then when it reaches zero the chase ends
			currentAlertness -= alertnessDecayRate * Time.deltaTime * 3f; // Reduce the alertness faster than normal
			if(currentAlertness <= 0f)
				EndChase();
		}
		if(timeFromLastNoise >= timeUntilAlertnessDecay) {
			currentAlertness -= alertnessDecayRate * Time.deltaTime;
			currentAlertness = Mathf.Max(currentAlertness, 0f);
		}
	
	}

	private void StartChase() {
		chasing = true;
		hand.StartChase();
	}
	private void EndChase() {
		chasing = false;
		alertnessLevel = 4;
		currentAlertness = 0f;
		hand.EndChase();
	}

	private Room currentRoom;

	public void SetCurrentRoom(Room r) {
		currentRoom = r;
	}

	public void SpawnHand(Vector3 pos) {
		GameObject h = Instantiate(handPrefab, pos, Quaternion.identity) as GameObject;
		h.GetComponent<FollowingHand>().SetPlayer(player);
		h.GetComponent<FollowingHand>().StartChase();
	}

	public void HearNoise(float noiseLevel) {
		if(RoomManager.Instance.CurrentRoom != null) {
			RoomManager.Instance.CurrentRoom.NoiseHeard(noiseLevel);
		}
		//if(currentRoom != null) {
		//	currentRoom.NoiseHeard(noiseLevel);
		//}

		/*
		if(!chasing) {
			timeFromLastNoise = 0f;
			currentAlertness += noiseLevel;
			if(currentAlertness >= maxAlertness) {
				alertnessLevel++;
				if(alertnessLevel == 5) {
					currentAlertness = maxAlertness;
					StartChase();
				}
				else {
					currentAlertness = 0f;
				}
			}
		}*/
	}
}
