using UnityEngine;
using System.Collections;

public class ComboEffects : MonoBehaviour {
	[SerializeField]
	private GameObject firstEffect;
	[SerializeField]
	private GameObject secondEffect;
	[SerializeField]
	private GameObject thirdEffect;

	private ComboHandler combos;

	private bool effectsStarted = false;

	// Use this for initialization
	void Start () {
		combos = GetComponent<ComboHandler>();
		ResetEffects();
	}
	
	// Update is called once per frame
	void Update () {
		if(effectsStarted && combos.TotalCombo < 10) {
			// Oh snap, this means we must reset!
			ResetEffects();
		}
	}

	private void ResetEffects() {
		WorldBackground.Instance.StopColors();

		effectsStarted = false;
		firstEffect.GetComponent<ParticleSystem>().Stop();
		secondEffect.GetComponent<ParticleSystem>().Stop();
		//thirdEffect.GetComponent<TrailRenderer>().enabled = false;
		thirdEffect.transform.parent = null;
		CancelInvoke("EffectActivator");
		StartCoroutine(EffectActivator());
	}

	private IEnumerator EffectActivator() {
		while(combos.TotalCombo < 10) {
			yield return null;
		}

		effectsStarted = true;
		Debug.Log("WHOO 10!!");
		firstEffect.GetComponent<ParticleSystem>().Play();

		WorldBackground.Instance.StartColors();

		while(combos.TotalCombo < 20) {
			yield return null;
		}

		Debug.Log("YEEEAH 20!!");
		secondEffect.GetComponent<ParticleSystem>().Play();

		while(combos.TotalCombo < 30) {
			yield return null;
		}

		Debug.Log("HOLY SHIT 30!!");
		thirdEffect.GetComponent<TrailRenderer>().enabled = false;
		thirdEffect.transform.parent = this.transform;
		thirdEffect.transform.position = this.transform.position + new Vector3(0f, 0.44f, 0f);
		thirdEffect.GetComponent<TrailRenderer>().enabled = true;
	}
}
