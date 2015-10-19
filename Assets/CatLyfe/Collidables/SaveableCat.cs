using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CollidableObject))]
public class SaveableCat : MonoBehaviour {
	[SerializeField]
	private GameObject cage;

	[SerializeField]
	private GameObject cat;

	void OnCollisionWithPlayer(GameObject player) {
		gameObject.layer = LayerMask.NameToLayer("Broken"); // So player can't collide anymore

		Destroy(cage);

		// Mark cat as saved
		LevelManager.Instance.SetCatSaved(MainSceneLoader.currentLevelName, true);

		CollidableObject col = GetComponent<CollidableObject>();
		player.SendMessage("AddScore", new ScoreEvent(col.ScoreValue, ScoreType.SAVE, col.ScoreText, gameObject.transform.position));

		cat.GetComponent<Animator>().SetTrigger("Saved");
		StartCoroutine(WaitForAnimationToBeDone());
	}

	private IEnumerator WaitForAnimationToBeDone() {
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
}
