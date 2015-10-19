using UnityEngine;
using System.Collections;

public class OpeningSequence : MonoBehaviour {
	[SerializeField]
	private GameObject tv;
	[SerializeField]
	private GameObject logoThump;
	[SerializeField]
	private GameObject cameraObject;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LogoAnimDone() {
		StartCoroutine(Sequence());
	}

	private IEnumerator Sequence() {
		tv.GetComponent<Animator>().SetTrigger("Breaking");
		yield return new WaitForSeconds(0.2f);
		logoThump.SetActive(true);
		Camera.main.GetComponent<CameraEffects>().Rumble(0.5f);
		yield return new WaitForSeconds(3f);
		Camera.main.GetComponent<ObjectFollower>().enabled = false;
		cameraObject.GetComponent<Animator>().SetTrigger("Fall");
	}
}
