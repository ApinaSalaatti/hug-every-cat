using UnityEngine;
using System.Collections;

public class CollidableObject : MonoBehaviour {
	[SerializeField]
	private string[] scoreTexts = new string[] { "Scored!" };
	[SerializeField]
	private float noiseLevel = 1f;
	[SerializeField]
	private int scoreValue = 100;

	public string ScoreText {
		get {
			int r = Random.Range(0, scoreTexts.Length);
			return scoreTexts[r];
		}
	}
	public int ScoreValue { get { return scoreValue; } }

	private int playerLayer;

	// Use this for initialization
	void Start () {
		playerLayer = LayerMask.NameToLayer("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		//Debug.Log("COLLISION WITH " + col.gameObject.name);
		if(col.gameObject.layer == playerLayer) {
			gameObject.SendMessage("OnCollisionWithPlayer", col.gameObject);
			HumanAlertness.Instance.HearNoise(noiseLevel);
		}
	}


}
