using UnityEngine;
using System.Collections;

public class LevelSelectButton : MonoBehaviour {
	[SerializeField]
	private string level;

	[SerializeField]
	private LevelInfo info;

	private Vector3 normalPos;
	private Vector3 upPos;

	private Vector3 targetPos;

	// Use this for initialization
	void Start () {
		normalPos = transform.position;
		upPos = transform.position + new Vector3(0f, 0.03f, 0f);
		targetPos = normalPos;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime*3f);
	}

	public void OnMouseEnter() {
		//Debug.Log("MOUSE IN " + gameObject.name);
		targetPos = upPos;
		info.SetLevel(level);
	}
	public void OnMouseExit() {
		//Debug.Log("MOUSE OUT " + gameObject.name);
		targetPos = normalPos;
	}

	public void OnMouseClick() {
		LevelManager.Instance.OpenLevel(level);
	}
}
