using UnityEngine;
using System.Collections;

public class FollowingHand : MonoBehaviour {
	[SerializeField]
	private GameObject player;
	public void SetPlayer(GameObject p) {
		player = p;
	}

	private float speed = 8f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 toPlayer = player.transform.position - transform.position;
		if(toPlayer.magnitude > 1f) {
			toPlayer = toPlayer.normalized;
			transform.position += toPlayer * 8f * Time.deltaTime;
			transform.rotation = Quaternion.LookRotation(toPlayer);
		}
		else {
			GameState.Instance.GameOver();
			EndChase();
		}
	}

	public void StartChase() {
		//gameObject.transform.position = player.transform.position + new Vector3(0f, 3f, -15f);
		gameObject.SetActive(true);
	}
	public void EndChase() {
		gameObject.SetActive(false);
	}
}
