using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfirmDialog : MonoBehaviour {
	private static ConfirmDialog instance;
	public static ConfirmDialog Instance { get { return instance; } }

	[SerializeField]
	private Text questionText;

	private System.Action ok;

	// Use this for initialization
	void Awake() {
		instance = this;
	}

	public void OnOk() {
		ok();
		Close();
	}
	public void OnCancel() {
		Close();
	}

	private void Close() {
		transform.localScale = Vector3.zero;
	}

	public void Show(string question, System.Action okAction) {
		questionText.text = question;
		ok = okAction;
		transform.localScale = new Vector3(1f, 1f, 1f);
	}
}
